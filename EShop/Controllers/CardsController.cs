using Eshop.Data;
using Eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Controllers
{
    public class CardsController : Controller
    {
        private readonly EshopContext _context;

        public CardsController(EshopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var carts = _context.Carts
                .Where(c => c.AccountId == 3)
                .Include(p => p.Product)
                .ToList();
            ViewBag.Total = _context.Carts
                .Where(c=>c.AccountId ==3)
                .Sum(c => c.Quantity * c.Product.Price);
            return View(carts);
        }

        [HttpPost]
        public IActionResult Add(int productId, int quantity = 1)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.ProductId == productId && c.AccountId == 3);
            if (cart == null)
            {
                cart = new Cart()
                {
                    ProductId = productId,
                    AccountId = 3,
                    Quantity = 1
                };
                _context.Carts.Add(cart);
            }
            else
            {
                cart.Quantity += quantity;
                _context.Carts.Update(cart);
            }

            _context.SaveChanges();
            _context.Dispose();

            return RedirectToAction("index");
        }
    }
}
