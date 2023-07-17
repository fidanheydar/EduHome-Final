using EduHome.App.Context;
using EduHome.App.Extensions;
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
            if (!course.FormFile.ContentType.Contains("image")) 
            {
                ModelState.AddModelError("FormFile", "Select correctly !"); 
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
            if (!ModelState.IsValid) return View(course);
            var existCourse = await _dbContext.Courses.FindAsync(id);
            if (existCourse == null)
            {
                return NotFound();
            }
            if (course.FormFile != null)
            {
                existCourse.Image = course.FormFile
                        .CreateImage(_env.WebRootPath, "assets/img/");
            }
            existCourse.Name = course.Name;
            existCourse.Description = course.Description;
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
