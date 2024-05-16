using Buoi2_Layout.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi2_Layout.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student(){Id = 1, Name = "Nguyen Van A", GPA = 5.2});
            students.Add(new Student(){Id = 2, Name = "Tran Van B", GPA = 7.2});
            students.Add(new Student(){Id = 3, Name = "Nguyen Thi C", GPA = 3.2});
            return View(students);
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
