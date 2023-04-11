using ELearn.Data;
using ELearn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = await _context.Courses.Include(m=>m.CourseImages).Include(m=>m.Owner).Where(m => m.SoftDelete == false).OrderByDescending(m=>m.Id).ToListAsync();
            return View(courses);
        }

        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest(); 
            Course course = await _context.Courses.Include(m => m.CourseImages).Include(m => m.Owner).Where(m => m.SoftDelete == false).FirstOrDefaultAsync(m => m.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
