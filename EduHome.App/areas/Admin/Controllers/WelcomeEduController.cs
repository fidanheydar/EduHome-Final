using EduHome.App.Context;
using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WelcomeEduController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;
        public WelcomeEduController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<WelcomeEdu> welcomeEdus = await _context.WelcomeEdus.Where(x => !x.IsDeleted).ToListAsync();
            return View(welcomeEdus);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create(WelcomeEdu? welcomeEdu)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (welcomeEdu.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "The field image is required");
                return View();
            }
            if (!Helper.IsImage(welcomeEdu.FormFile))
            {
                ModelState.AddModelError("FormFile", "File type is not correct !");
                return View();
            }
            if (!Helper.IsSizeOk(welcomeEdu.FormFile, 1))
            {
                ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                return View();
            }
            welcomeEdu.Image = welcomeEdu.FormFile.CreateImage(_env.WebRootPath, "assets/img");
            welcomeEdu.CreatedDate = DateTime.Now;
            await _context.AddAsync(welcomeEdu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            WelcomeEdu? welcomeEdu= _context .WelcomeEdus.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (welcomeEdu == null)
            {
                return NotFound();
            }
            return View(welcomeEdu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(WelcomeEdu welcomeEdu,int id )
        {

            WelcomeEdu updatedWelcomeEdu = await _context.WelcomeEdus.FindAsync(id);
            if (welcomeEdu == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(updatedWelcomeEdu);
            }
            if (welcomeEdu.FormFile != null)
            {
                if (!Helper.IsImage(welcomeEdu.FormFile))
                {
                    ModelState.AddModelError("FormFile", "File type is not correct !");
                    return View();
                }
                if (!Helper.IsSizeOk(welcomeEdu.FormFile, 1))
                {
                    ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                    return View();
                }

                updatedWelcomeEdu.Image = welcomeEdu.FormFile
                    .CreateImage(_env.WebRootPath, "assets/img");
            }

            updatedWelcomeEdu.UpdatedDate = DateTime.Now;
            updatedWelcomeEdu.Description = welcomeEdu.Description;
            updatedWelcomeEdu.Title = welcomeEdu.Title;
            updatedWelcomeEdu.Link= welcomeEdu.Link;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            WelcomeEdu? welcomeEdu = await _context.WelcomeEdus.FindAsync(id);
            welcomeEdu.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
    }
