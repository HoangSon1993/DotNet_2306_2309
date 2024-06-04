using Eshop.Data;
using Eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EshopContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(EshopContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.ProductType)
                .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("SKU,Name, Description,Price,Stock,ProductTypeId, ImageFile")] Product product)
        {
            // Data validation

            // Xử lý tên sản phẩm k trùng nhau.
            if (_context.Products.Any(p => p.Name == product.Name))
            {
                ViewBag.ErrorMsg = "Tên sản phẩm đã tồn tại";
                return View();
            }

            // Kiểm tra định dạng ảnh
            if (!product.ImageFile.ContentType.StartsWith("image"))
            {
                ViewBag.ErrorMsg = "Chỉ được upload file ảnh";
                return View();
            }

            // Kiểm tra dung lượng file < 150kb
            if (product.ImageFile.Length > 150 * 1024)
            {
                ViewBag.ErrorMsg = "Dung lượng file tối đa 150kb";
                return View();
            }

            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name");

            _context.Products.Add(product);
            _context.SaveChanges();

            // Xử lý upload file
            if (product.ImageFile != null)
            {
                // lấy ra phần mở rộng
                // var fileExtension = Path.GetExtension(product.ImageFile.FileName);

                // Tên file bằng id + phần mở rộng
                var fileName = product.Id + Path.GetExtension(product.ImageFile.FileName);

                // lấy đường dẫn gốc tới thư mục images /product
                var uploadFolder = Path.Combine(_environment.WebRootPath, "images", "product");

                // Đường dẫn upload = image/product/[ten_file]+[phan_mo_rong]
                var uploadPath = Path.Combine(uploadFolder, fileName);

                // Tien hanh upload file
                using (FileStream fs = System.IO.File.Create(uploadPath))
                {
                    product.ImageFile.CopyTo(fs);
                    fs.Flush();
                }

                // Sau khi upload, cập nhật lại tên file trong db
                product.Image = fileName;
                _context.Products.Update(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // product.ProductType = _context.ProductTypes.FirstOrDefault(x => x.Id == product.ProductTypeId);

            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductTypeId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int? productId,
            [Bind("Id,SKU,Name, Description,Price,Stock,ProductTypeId")]
            Product product)
        {
            if (productId != product.Id)
            {
                return NotFound();
            }

            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            //change Status

            // if (id == null)
            // {
            //     return NotFound();
            // }
            //
            // var product = _context.Products.FirstOrDefault(p => p.Id == id);
            // if (product != null) _context.Products.Remove(product);
            // _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult List()
        {
            var products = _context.Products
                .Where(p => p.Status)
                .ToList();
            return View(products);
        }
    }
}