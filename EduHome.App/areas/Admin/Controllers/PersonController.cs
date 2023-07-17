using EduHome.App.Context;
using EduHome.App.Extensions;
using EduHome.App.Helpers;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController : Controller
    {
        private readonly EduHomeDbContext _dbContext;
        private readonly IWebHostEnvironment _env;


        public PersonController(EduHomeDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Person> people = await _dbContext.People.Where(x => !x.IsDeleted).ToListAsync();
            return View(people);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            if (person.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "The field image is required");
                return View();
            }
            if (!Helper.IsImage(person.FormFile))
            {
                ModelState.AddModelError("FormFile", "File type is not correct !");
                return View();
            }
            if (!Helper.IsSizeOk(person.FormFile, 1))
            {
                ModelState.AddModelError("FormFile", "File size can not be over 1 mb!");
                return View();
            }
            person.Image = person.FormFile.CreateImage(_env.WebRootPath, "assets/img/");
            person.CreatedDate = DateTime.Now;
            await _dbContext.People.AddAsync(person);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("index", "course");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Person? person = _dbContext.People
                .Where(x => !x.IsDeleted && x.Id == id)
                .FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Person updatedPerson,int id)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedPerson);
            }
            Person existPerson = await _dbContext.People.FindAsync(id);

            if (existPerson == null)
            {
                return NotFound();
            }
            updatedPerson.UpdatedDate = DateTime.Now;
            existPerson.Description = updatedPerson.Description;
            existPerson.Name = updatedPerson.Name;
           
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
