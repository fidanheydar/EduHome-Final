using EduHome.App.Context;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherDegreeController : Controller
    {
        private readonly EduHomeDbContext _context;
        public TeacherDegreeController(EduHomeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<TeacherDegree> teacherDegrees = await _context.TeacherDegrees.Where(x => !x.IsDeleted)
              .ToListAsync();
            return View(teacherDegrees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherDegree teacherdegree)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _context.AddAsync(teacherdegree);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "TeacherDegree");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            TeacherDegree? degree  = await _context.TeacherDegrees
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();
            if (degree == null)
                return NotFound();
            return View(degree);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, TeacherDegree uptdegree)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TeacherDegree? degree = await _context.TeacherDegrees
            .Where(x => !x.IsDeleted && x.Id == id)
            .FirstOrDefaultAsync();
            if (degree == null)
                return NotFound();
            degree.Name = uptdegree.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TeacherDegree? degree = await _context.TeacherDegrees
                    .Where(x => !x.IsDeleted && x.Id == id)
                     .FirstOrDefaultAsync();
            if (degree == null)
                return NotFound();
            degree.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
