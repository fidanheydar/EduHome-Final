using EduHome.App.Context;
using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {

        private readonly EduHomeDbContext  _dbContext;
        private readonly IWebHostEnvironment _env;

        public CourseController(EduHomeDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }
        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Course> courses = await _dbContext.Courses.Where(x => !x.IsDeleted)
               .ToListAsync();
            return View(courses);
        }
        [HttpGet]
        public  async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (course.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "The field image is required");
                return View();
            }
            if (!Helper.IsImage(course.FormFile))
            {
                ModelState.AddModelError("FormFile", "File type is not correct !");
                return View();
            }
            if (!Helper.IsSizeOk(course.FormFile, 1))
            {
                ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                return View();
            }
            course.Image = course.FormFile.CreateImage(_env.WebRootPath, "assets/img/");
            course.CreatedDate = DateTime.Now;
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("index", "course");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Course? course = _dbContext.Courses
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Course course, int id)
        {
            Course? updateCourse = _dbContext.Courses
                 .Where(x => !x.IsDeleted && x.Id == id)
                 .FirstOrDefault();
            if (course == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updateCourse);
            }

            if (course.FormFile != null)
            {
                if (!Helper.IsImage(course.FormFile))
                {
                    ModelState.AddModelError("FormFile", "File type is not correct !");
                    return View();
                }
                if (!Helper.IsSizeOk(course.FormFile, 1))
                {
                    ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                    return View();
                }

                updateCourse.Image = course.FormFile
                    .CreateImage(_env.WebRootPath, "assets/img");
            }
            updateCourse.Name = course.Name;
            updateCourse.Description = course.Description;
            course.UpdatedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("index", "course");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Course? course = await _dbContext.Courses.FindAsync(id);
            course.IsDeleted = true;
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
