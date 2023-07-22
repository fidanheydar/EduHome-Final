using EduHome.App.Context;
using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController :Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CourseController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()//bdenede bele yoxla onda  heyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyb
        {
            IEnumerable<Course> courses = await _context.Courses.Where(x => !x.IsDeleted)
                .Include(x => x.CLanguage)
                .ToListAsync();
            return View(courses);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Languages = await _context.CLanguages.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.CourseAssests = await _context.CAssets.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Languages = await _context.CLanguages.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.CourseAssests = await _context.CAssets.Where(x => !x.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(course);
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
            foreach (var item in course.CategoryIds)
            {
                if (!await _context.Categories.AnyAsync(x => x.Id == item))
                {
                    ModelState.AddModelError("", "Invalid Category Id");
                    return View(course);
                }
                CourseCategory courseCategory = new CourseCategory
                {

                    Course = course,
                    CategoryId = item,
                    CreatedDate = DateTime.Now,
                };
                await _context.CourseCategories.AddAsync(courseCategory);
            }
            foreach (var item in course.TagIds)
            {
                if (!await _context.Tags.AnyAsync(x => x.Id == item))
                {
                    ModelState.AddModelError("", "Invalid Tag Id");
                    return View(course);
                }
                CourseTag courseTag = new CourseTag
                {
                    Course = course,
                    TagId = item,
                    CreatedDate = DateTime.Now
                };
                await _context.CourseTags.AddAsync(courseTag);
            }
            course.Image = course.FormFile.CreateImage(_env.WebRootPath, "assets/img");//yoxla bdenede
            course.CreatedDate = DateTime.Now;
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Languages = await _context.CLanguages.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.CourseAssests = await _context.CAssets.Where(x => !x.IsDeleted).ToListAsync();
            Course? course = await _context.Courses.Where(x => !x.IsDeleted && x.Id == id)
           .AsNoTracking()
           .Include(x => x.courseCategories).ThenInclude(x => x.Category)
           .Include(x => x.courseTags).ThenInclude(x => x.Tag)
           .Include(x => x.CLanguage)
           .Include(x => x.CAssets)
           .FirstOrDefaultAsync();

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Course course)
        {
            Course? uptcourse = await _context.Courses.Where(x => !x.IsDeleted && x.Id == id)
                             .AsNoTracking()
                             .Include(x => x.courseCategories).ThenInclude(x => x.Category)
                             .Include(x => x.courseTags).ThenInclude(x => x.Tag)
                             .Include(x => x.CLanguage)
                             .Include(x => x.CAssets)
                             .FirstOrDefaultAsync();

            if (course is null)
            {
                return View(course);
            }
            if (!ModelState.IsValid)
            {
                return View(uptcourse);
            }

            if (course.FormFile != null)
            {
                if (!Helper.IsImage(course.FormFile))
                {
                    ModelState.AddModelError("FormFile", "File type is not correct !");
                    return View(course);
                }
                if (!Helper.IsSizeOk(course.FormFile, 1))
                {
                    ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                    return View(course);
                }
                Helper.RemoveImage(_env.WebRootPath, "assets/img", uptcourse.Image);
                course.Image = course.FormFile.CreateImage(_env.WebRootPath, "assets/img");
                  }
            else
            {
                course.Image = uptcourse.Image;
            }
                List<CourseCategory> RmvCategory = await _context.CourseCategories.
                Where(x => !course.CategoryIds.Contains(x.CategoryId)).ToListAsync();

                _context.CourseCategories.RemoveRange(RmvCategory);
                foreach (var item in course.CategoryIds)
                {
                    if (_context.CourseCategories.Where(x => x.CourseId == id && x.CategoryId == item).Count() > 0)
                        continue;

                    await _context.CourseCategories.AddAsync(new CourseCategory
                    {
                        CategoryId = item,
                        CourseId = id
                    });
                }
                List<CourseTag> RmvTag = await _context.CourseTags.
                 Where(x => !course.TagIds.Contains(x.TagId)).ToListAsync();
                _context.CourseTags.RemoveRange(RmvTag);

                foreach (var item in course.TagIds)
                {
                    if (_context.CourseTags.Where(x => x.CourseId == id && x.TagId == item).Count() > 0)
                        continue;

                    await _context.CourseTags.AddAsync(new CourseTag
                    {
                        TagId = item,
                        CourseId = id
                    });
                }
                uptcourse.Name = course.Name;
                uptcourse.Description = course.Description;
                uptcourse.UpdatedDate = DateTime.Now;
                uptcourse.AboutCourse = course.AboutCourse;
                uptcourse.Apply = uptcourse.Apply;
                uptcourse.CourseFee = course.CourseFee;
                uptcourse.SkillLevel = course.SkillLevel;
                uptcourse.StartDate = course.StartDate;
                uptcourse.EndDate = course.EndDate;
                uptcourse.CourseLanguageId = course.CourseLanguageId;
                uptcourse.CAssetsId = course.CAssetsId;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        public async Task<IActionResult> Delete(int id)
        {
            Course? course = await _context.Courses.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return NotFound();
            }
            course.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
    }


