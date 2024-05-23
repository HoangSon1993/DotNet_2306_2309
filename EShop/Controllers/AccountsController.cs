using Eshop.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class AccountsController : Controller
    {
        private EshopContext _context;

        public AccountsController(EshopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var accounts = _context.Accounts.ToList();
            return View(accounts);
        }
        
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var accounts = _context.Accounts.FirstOrDefault(a=>a.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }
    }
}
