using EduHome.App.Context;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NoticeController : Controller
    {
        private readonly EduHomeDbContext _dbContext;

        public NoticeController(EduHomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Notice> Notices = await _dbContext.Notices.Where(x => !x.IsDeleted)
                .ToListAsync();
            return View(Notices);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            notice.CreatedDate = DateTime.Now;
            await _dbContext.Notices.AddAsync(notice);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("index", "notice");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Notice? notice = await _dbContext.Notices.FindAsync(id);
            notice.IsDeleted = true;
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Notice? notice = _dbContext.Notices
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (notice == null)
            {
                return NotFound();
            }
            return View(notice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Notice updatedNotice, int id)
        {

            Notice? notice = _dbContext.Notices
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (notice == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return  View(updatedNotice);
            }
            Notice existNotice = await _dbContext.Notices.FindAsync(id);
            if (existNotice == null)
            {
                return NotFound();
            }
            updatedNotice.UpdatedDate = DateTime.Now;
            notice.Description = updatedNotice.Description;
            notice.Date=updatedNotice.Date;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

