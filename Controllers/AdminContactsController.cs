using coffeeshop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coffeeshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminContactsController : Controller
    {
        private readonly CoffeeshopDbContext dbContext;

        public AdminContactsController(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await dbContext.ContactMessages
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View(contacts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var contact = await dbContext.ContactMessages.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            if (!contact.IsRead)
            {
                contact.IsRead = true;
                await dbContext.SaveChangesAsync();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await dbContext.ContactMessages.FindAsync(id);

            if (contact != null)
            {
                dbContext.ContactMessages.Remove(contact);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}