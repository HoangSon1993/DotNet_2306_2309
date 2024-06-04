using Eshop.Data;
using Eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly EshopContext _context;


        public InvoicesController(EshopContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Xem DS HD đã mua
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var invoices = _context.Invoices.Where(i => i.AccountId == 3).ToList();

            return View(invoices);
        }

        [HttpPost]
        public IActionResult Add(string ShippingAddress, string ShippingPhone)
        {
            // Lấy ds Sản phẩm trong Cart
            var cart = _context.Carts
                                        .Include(c => c.Product)
                                        .Where(c => c.AccountId == 3)
                                        .ToList();

            //Tạo hoá đơn
            Invoice invoice = new Invoice()
            {
                AccountId = 3,
                Code = "123456789123",
                ShippingAddress = ShippingAddress,
                ShippingPhone = ShippingPhone,
                Total = cart.Sum(c => c.Quantity * c.Product.Price)
            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            // sau khi saveChange() sẽ có được invoiceId

            //Tạo invoiceDetail từ cart
            foreach (var item in cart)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail()
                {
                    InvoiceId = invoice.Id,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                };

                // Thêm invoiceDetail vào db
                _context.InvoiceDetails.Add(invoiceDetail);
                // Xoá product trong cart
                _context.Carts.Remove(item);
                // Xoá tồn kho
                item.Product.Stock -= item.Quantity;
                _context.Products.Update(item.Product);
            }

            _context.SaveChanges();
            _context.Dispose();
            return RedirectToAction("Index", "Products");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var invoiceDetails= _context.InvoiceDetails
                .Include(i=>i.Product)
                .Include(i=>i.Invoice)
                .Where(i => i.InvoiceId == id).ToList();
            ViewBag.Total = invoiceDetails.Sum(i => i.Quantity * i.Product.Price);

            return View(invoiceDetails);
        }
    }
}
