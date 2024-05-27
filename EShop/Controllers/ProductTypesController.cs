using Eshop.Data;
using Eshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly EshopContext _context;

        public ProductTypesController(EshopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var productTypes = _context.ProductTypes.ToList();
            return View(productTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name")] ProductType productType)
        {
            if (_context.ProductTypes.Any(p => p.Name == productType.Name))
            {
                ViewBag.ErrorMsg = "Tên ProductTypes không được trùng";
                return View();
            }
            _context.ProductTypes.Add(productType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _context.ProductTypes.FirstOrDefault(p => p.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _context.ProductTypes.FirstOrDefault(p => p.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        public IActionResult Delele()
        {
            throw new NotImplementedException();
        }


    }
}
