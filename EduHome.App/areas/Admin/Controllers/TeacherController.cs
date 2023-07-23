using EduHome.App.Context;
using EduHome.App.Extensions;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly EduHomeDbContext _context;

        private readonly IWebHostEnvironment _env;
        public TeacherController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Networks = _context.networks.ToList();
            ViewBag.Hobbies = _context.Hobbies.ToList();
            var teachers = await _context.Teachers.Where(x => !x.IsDeleted).
                Include(x => x.TeacherHobbies).
                ToListAsync();
            return View(teachers);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Networks = _context.networks.Where(x => !x.IsDeleted).ToList();
            ViewBag.Hobbies = _context.Hobbies.Where(x => !x.IsDeleted).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher, IFormFile file)
        {
            teacher.FormFile = file;
            ViewBag.Networks = _context.networks.Where(x => !x.IsDeleted).ToList();
            ViewBag.Hobbies = _context.Hobbies.Where(x => !x.IsDeleted).ToList();


            if (!ModelState.IsValid)
            {
                return View(teacher);
            }


            if (!teacher.FormFile.ContentType.Contains("image"))//yanlish extention ile file daxil edilmesinin qarshisinin alinmasi uchun
            {
                ModelState.AddModelError("FormFile", "Duzgun daxil etmemisiniz"); //error mesaji qaytarmaq uchun
            }


            teacher.Image = teacher.FormFile.CreateImage(_env.WebRootPath, "assets/img/");

            foreach (var item in teacher.HobbyIds)
            {
                if (!await _context.Hobbies.AnyAsync(x => x.Id == item))
                {
                    ModelState.AddModelError("", "-----");
                    return View(teacher);
                }
                TeacherHobby teacherHobbies = new TeacherHobby
                {
                    HobbyId = item,
                    Teacher = teacher,
                    CreatedDate = DateTime.Now
                };
                await _context.TeacherHobbies.AddAsync(teacherHobbies);

            }

            teacher.CreatedDate = DateTime.Now;
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();


            return RedirectToAction("index", "teacher");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Teacher? teacher  = await _context.Teachers
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefaultAsync();
            if (teacher == null)
                return NotFound();//teacher degreee niyeee eeee nebilimee vayrdi
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Teacher? exteacher = await _context.Teachers
            .Where(x => !x.IsDeleted && x.Id == id)
            .FirstOrDefaultAsync();
            if (teacher == null)
                return NotFound();
            exteacher.Fullname = teacher.Fullname;
            exteacher.About = teacher.About;//bunnari ozun duzeldersen
            exteacher.Fullname = teacher.Fullname;
            exteacher.Fullname = teacher.Fullname;
            exteacher.Fullname = teacher.Fullname;
            exteacher.Fullname = teacher.Fullname;
            exteacher.Fullname = teacher.Fullname;
            exteacher.Fullname = teacher.Fullname;
            exteacher.Fullname = teacher.Fullname;
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
            Teacher? teacher = await _context.Teachers
                    .Where(x => !x.IsDeleted && x.Id == id)
                     .FirstOrDefaultAsync();
            if (teacher == null)
                return NotFound();
            teacher.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
