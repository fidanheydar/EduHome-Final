using EduHome.App.Context;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class CAssetsController : Controller
    {
        private readonly EduHomeDbContext _context;

        public CAssetsController(EduHomeDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            var assets = _context.CAssets.Where(x=>!x.IsDeleted).ToList();
            return View(assets);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CAssets cassets)
        {
            if (!ModelState.IsValid)
            {
                return View(cassets);
            }
            cassets.CreatedDate = DateTime.Now;
            await _context.AddAsync(cassets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            CAssets? cassets = await _context.CAssets
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            if (cassets == null)
            {
                return NotFound();
            }
            return View(cassets);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CAssets CAssets)
        {
            CAssets? uptcassets = await _context.CAssets
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (CAssets == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(CAssets);
            }

            uptcassets.Name = CAssets.Name;
            uptcassets.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {

            CAssets? Cassets = await _context.CAssets
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            if (Cassets == null)
            {
                return NotFound();
            }

            Cassets.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}