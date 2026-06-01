using coffeeshop.Data;
using coffeeshop.Models;
using coffeeshop.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coffeeshop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly CoffeeshopDbContext dbContext;

        public OrdersController(
            IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository,
            UserManager<IdentityUser> userManager,
            CoffeeshopDbContext dbContext)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public IActionResult Checkout()
        {
            var items = shoppingCartRepository.GetAllShoppingCartItems();

            if (!items.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = shoppingCartRepository.GetAllShoppingCartItems();

            if (!items.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            order.UserId = userManager.GetUserId(User);
            order.UserEmail = userManager.GetUserName(User);
            order.Status = "Success";

            orderRepository.PlaceOrder(order);

            shoppingCartRepository.ClearCart();
            HttpContext.Session.SetInt32("CartCount", 0);

            return RedirectToAction("CheckoutComplete");
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }

        public async Task<IActionResult> UserOrders()
        {
            var userId = userManager.GetUserId(User);

            var orders = await dbContext.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails!)
                    .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderPlaced)
                .ToListAsync();

            return View(orders);
        }
    }
}