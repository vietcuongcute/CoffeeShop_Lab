using coffeeshop.Data;
using coffeeshop.Models;
using coffeeshop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace coffeeshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly CoffeeshopDbContext dbContext;

        public HomeController(
            IProductRepository productRepository,
            CoffeeshopDbContext dbContext)
        {
            this.productRepository = productRepository;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = productRepository.GetTrendingProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View(new ContactMessage());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMessage contactMessage)
        {
            if (!ModelState.IsValid)
            {
                return View(contactMessage);
            }

            contactMessage.CreatedAt = DateTime.Now;
            contactMessage.IsRead = false;

            dbContext.ContactMessages.Add(contactMessage);
            await dbContext.SaveChangesAsync();

            TempData["ContactSuccess"] = "Your message has been sent successfully!";

            return RedirectToAction(nameof(Contact));
        }
    }
}