using EduHome.App.Context;
using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHome.App.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Slides.Where(x => !x.IsDeleted).ToListAsync();
            return View(sliders);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (slider.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "The field image is required");
                return View();
            }
            if (!Helper.IsImage(slider.FormFile))
            {
                ModelState.AddModelError("FormFile", "File type is not correct !");
                return View();
            }
            if (!Helper.IsSizeOk(slider.FormFile, 1))
            {
                ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                return View();
            }
            slider.Image = slider.FormFile.CreateImage(_env.WebRootPath, "assets/img");
            slider.CreatedDate = DateTime.Now;
            await _context.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Slider? slider = await _context.Slides
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();

            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Slider slider)
        {
            Slider? updateSlider = await _context.Slides
       .Where(x => !x.IsDeleted && x.Id == id)
       .FirstOrDefaultAsync();

            if (slider == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updateSlider);
            }
            if (slider.FormFile != null)
            {
                if (!Helper.IsImage(slider.FormFile))
                {
                    ModelState.AddModelError("FormFile", "File type is not correct !");
                    return View();
                }
                if (!Helper.IsSizeOk(slider.FormFile, 1))
                {
                    ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                    return View();
                }

                updateSlider.Image = slider.FormFile
                    .CreateImage(_env.WebRootPath, "assets/img");
            }

            //Helper.RemoveImage(_env.WebRootPath,"assets/images",blog.Image);

            updateSlider.Description = slider.Description;
            updateSlider.Title = slider.Title;
            updateSlider.CreatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider? slider = await _context.Slides
                        .Where(x => !x.IsDeleted && x.Id == id)
                            .FirstOrDefaultAsync();
            if (slider == null)
                return NotFound();

            slider.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
