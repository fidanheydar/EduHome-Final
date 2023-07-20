using EduHome.App.Context;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherPositionController :Controller
    {
        private readonly EduHomeDbContext _context;
        public TeacherPositionController(EduHomeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<TeacherPosition> teacherPositions = await _context.TeacherPositions.Where(x => !x.IsDeleted)
                .ToListAsync();
            return View(teacherPositions);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherPosition teacherposition)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _context.AddAsync(teacherposition);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","TeacherPosition");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            TeacherPosition? position = await _context.TeacherPositions
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();
            if (position == null)
                return NotFound();
            return View(position);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, TeacherPosition uptposition)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TeacherPosition? position = await _context.TeacherPositions
            .Where(x => !x.IsDeleted && x.Id == id)
            .FirstOrDefaultAsync();
            if (position == null)
                return NotFound();
            position.Name = uptposition.Name;
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
            TeacherPosition? position = await _context.TeacherPositions
                    .Where(x => !x.IsDeleted && x.Id == id)
                     .FirstOrDefaultAsync();
            if (position == null)
                return NotFound();
            position.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
