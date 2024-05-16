using Buoi1_DemoWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi1_DemoWebApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListAll()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){Id = 1 , Name = "John", GPA = 7.8},
                new Student(){Id = 2 , Name = "Eve", GPA = 4.9},
                new Student(){Id = 3 , Name = "Ricky", GPA = 10}
            };
            return View(students);
        }

    }
}
