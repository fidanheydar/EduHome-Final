using EduHome.App.Context;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class CLanguageController : Controller
    {
        private readonly EduHomeDbContext _context;
        public CLanguageController(EduHomeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CLanguage> cLanguages = await _context.CLanguages
           .Where(x => !x.IsDeleted).ToListAsync();
            return View(cLanguages);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CLanguage cLanguage)
        {

            if (!ModelState.IsValid)
            {
                return View(cLanguage);
            }
            cLanguage.CreatedDate = DateTime.Now;
            await _context.AddAsync(cLanguage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            CLanguage? cLanguage = await _context.CLanguages
            .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            if (cLanguage == null)
            {
                return NotFound();
            }
            return View(cLanguage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int id, CLanguage cLanguage)
        {
            CLanguage? uptclanguage = await _context.CLanguages
                    .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (!ModelState.IsValid)
            {
                return View(uptclanguage);
            }
            if (cLanguage == null)
            {
                return NotFound();
            }
            uptclanguage.UpdatedDate = DateTime.Now;
            uptclanguage.Name = cLanguage.Name;
            uptclanguage.Courses = cLanguage.Courses;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            CLanguage? cLanguage = await _context.CLanguages
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (cLanguage == null)
            {
                return NotFound();
            }
            cLanguage.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}