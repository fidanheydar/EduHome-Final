using EduHome.App.Context;
using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly EduHomeDbContext _dbContext;
        private readonly IWebHostEnvironment _env;


        public TeacherController(EduHomeDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Teacher> teachers = await _dbContext.Teachers
                .Where(x => !x.IsDeleted)
                .Include(x => x.TeacherPosition)
                .Include(x => x.TeacherDegree).ToListAsync();
            return View(teachers);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.TeacherPosition = await _dbContext.TeacherPositions.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.TeacherDegree = await _dbContext.TeacherDegrees.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Hobb = await _dbContext.Hobbies.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            ViewBag.TeacherPosition = await _dbContext.TeacherPositions.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.TeacherDegree = await _dbContext.TeacherDegrees.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Hobby= await _dbContext.Hobbies.Where(x => !x.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (teacher.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "The field image is required");
                return View();
            }
            if (!Helper.IsImage(teacher.FormFile))
            {
                ModelState.AddModelError("FormFile", "File type is not correct !");
                return View();
            }
            if (!Helper.IsSizeOk(teacher.FormFile, 1))
            {
                ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                return View();
            }
            foreach (var item in teacher.HobbyIds)
            {
                if (!await _dbContext.Hobbies.AnyAsync(x => x.Id == item))
                {
                    ModelState.AddModelError("", "Invalid Hobby Id");
                    return View(teacher);
                }
                TeacherHobby teacherHobby = new TeacherHobby
                {
                    CreatedDate = DateTime.Now,
                    Teacher = teacher,
                    HobbyId = item,
                };
                await _dbContext.TeacherHobbies.AddAsync(teacherHobby);
            }
            teacher.Image = teacher.FormFile.CreateImage(_env.WebRootPath, "assets/img");
            teacher.CreatedDate = DateTime.Now;
            await _dbContext.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.TeacherPosition = await _dbContext.TeacherPositions.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.TeacherDegree = await _dbContext.TeacherDegrees.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Hobby = await _dbContext.Hobbies.Where(x => !x.IsDeleted).ToListAsync();
            Teacher? teacher = await _dbContext.Teachers
               .Where(x => !x.IsDeleted && x.Id == id)
                .Include(x => x.TeacherDegree)
                .Include(x => x.TeacherPosition)
                    .Include(x => x.TeacherHobbies)
                    .ThenInclude(x => x.Hobby)
                .FirstOrDefaultAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Teacher teacher)
        {
            ViewBag.TeacherPosition = await _dbContext.TeacherPositions.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.TeacherDegree = await _dbContext.TeacherDegrees.Where(x => !x.IsDeleted).ToListAsync();
            ViewBag.Hobby = await _dbContext.Hobbies.Where(x => !x.IsDeleted).ToListAsync();


        Teacher? updatedTeacher = await _dbContext.Teachers
                .Where(x => !x.IsDeleted && x.Id == id)
                .Include(x => x.TeacherDegree)
                .Include(x => x.TeacherPosition)
                .Include(x => x.TeacherHobbies)
                    .ThenInclude(x => x.Hobby)
                .FirstOrDefaultAsync();

            if (teacher == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(updatedTeacher);
            }
            if (teacher.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "The field image is required");
                return View();
            }
            if (!Helper.IsImage(teacher.FormFile))
            {
                ModelState.AddModelError("FormFile", "File type is not correct !");
                return View();
            }
            if (!Helper.IsSizeOk(teacher.FormFile, 1))
            {
                ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                return View();
            }
            updatedTeacher.Image = teacher.FormFile
                    .CreateImage(_env.WebRootPath, "assets/img");
            teacher.UpdatedDate =DateTime.Now;
            _dbContext.Update(teacher);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Teacher? teacher = await _dbContext.Teachers
                .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            teacher.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
   


