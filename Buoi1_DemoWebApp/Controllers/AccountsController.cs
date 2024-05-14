using Buoi1_DemoWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi1_DemoWebApp.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            List<Account> accounts = new List<Account>
            {
                new Account { Id = 1, UserName = "admin", Password = "admin" },
                new Account { Id = 2, UserName = "sonlh", Password = "son123" },
                new Account { Id = 3, UserName = "customer", Password = "123456" }
            };

            ViewBag.abcdef = accounts;

            return View();
        }
    }
}
