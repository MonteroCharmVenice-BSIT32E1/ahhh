using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAHH.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAHH.Controllers
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
