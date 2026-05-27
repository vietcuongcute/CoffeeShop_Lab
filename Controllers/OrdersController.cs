using coffeeshop.Models;
using coffeeshop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace coffeeshop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public OrdersController(
            IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
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

            orderRepository.PlaceOrder(order);

            shoppingCartRepository.ClearCart();

            HttpContext.Session.SetInt32("CartCount", 0);

            return RedirectToAction("CheckoutComplete");
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}