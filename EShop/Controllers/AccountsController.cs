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
        public IActionResult Create([Bind("Username, Passwork, Email")] Account account)
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

        public IActionResult Edit(int id)
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
        public IActionResult Edit(int? accountId,[Bind("Id, Username, Password, Email, Phone, Address, FullName, Avatar")] Account account)
        {
            if (accountId != account.Id)
            {
                return NotFound();
            }
            
            _context.Accounts.Update(account);
            _context.SaveChanges();

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
    }
}
