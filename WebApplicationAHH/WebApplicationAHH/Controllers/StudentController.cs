// Controllers/StudentController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentStrandAssessment.Data;
using StudentStrandAssessment.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudentStrandAssessment.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
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

// Controllers/DashboardController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentStrandAssessment.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudentStrandAssessment.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DashboardController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Teacher()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Student()
        {
            var user = await _userManager.GetUserAsync(User);
            var student = _context.Students.FirstOrDefault(s => s.Name == user.UserName);
            if (student != null)
            {
                ViewBag.Strand = student.DetermineStrand();
            }
            return View(student);
        }
    }
}
