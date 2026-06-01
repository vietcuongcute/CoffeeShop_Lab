using coffeeshop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coffeeshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly CoffeeshopDbContext dbContext;

        public AdminOrdersController(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderDetails)!
                .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderPlaced)
                .ToListAsync();

            return View(orders);
        }
    }
}