// Controllers/StudentController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAHH.Data;
using WebApplicationAHH.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAHH.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class StudentController : Controller
    {
        private readonly WebApplicationAHH _context;

        public StudentController(WebApplicationAHH context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Students.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
    }
}

