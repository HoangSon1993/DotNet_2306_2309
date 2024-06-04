using Eshop.Data;
using Eshop.Models;
using Eshop.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class AccountsController : Controller
    {
        private EshopContext _context;
        private readonly AccountService _accountService;

        public AccountsController(EshopContext context)
        {
            _context = context;
            _accountService = new AccountService(_context);
        }

        public IActionResult Index()
        {
            var accounts = _accountService.GetAllAccounts();
            // var accounts = _context.Accounts.ToList();
            return View(accounts);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var accounts = _context.Accounts.FirstOrDefault(a => a.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Username, Password, Email, Status")] Account account)
        {
            if (_context.Accounts.Any(a => a.Username == account.Username))
            {
                ViewBag.ErrorMsg = "Tài khoản đã có người sử dụng";
                return View();
            }
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var account = _context.Accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        public IActionResult Edit(int? accountId, [Bind("Id, Username, Password, Email, Status")] Account account)
        {
            if (accountId != account.Id)
            {
                return NotFound();
            }

            _context.Accounts.Update(account);
            _context.SaveChanges();

            // ViewBag.ErrorMsg = "";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = _context.Accounts.FirstOrDefault(a => a.Id == id);
            if (account != null) _context.Accounts.Remove(account);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Username, string Password)
        {
            var user = _context.Accounts.FirstOrDefault(a => a.Username == Username && a.Password == Password);
            if (user != null)
            {
                //CookieOptions options = new CookieOptions()
                //{
                //    Expires = DateTime.Now.AddSeconds(10)
                //};
                //HttpContext.Response.Cookies.Append("FullName", user.FullName, options);

                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetInt32("Id", user.Id);


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMsg = "Tài khoản hoặc mật khẩu không hợp lệ";
                return View();
            }
        }

        public IActionResult Logout()
        {
            //HttpContext.Response.Cookies.Delete("FullName");
            
            HttpContext.Session.Remove("FullName");
            return RedirectToAction("Index","Home");
        }
    }
}
