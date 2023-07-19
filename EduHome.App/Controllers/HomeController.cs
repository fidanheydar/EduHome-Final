using EduHome.Core.Entities;
using EduHome.App.ViewModels;
using EduHome.App.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHome.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduHomeDbContext _eduHomeDbContext;

        public HomeController(EduHomeDbContext eduHomeDbContext)
        {
            _eduHomeDbContext = eduHomeDbContext;
        }
        public  async Task<IActionResult> Index()
        {
            var model = new ViewModel();
            model.notices = _eduHomeDbContext.Notices.Where(x => !x.IsDeleted).ToList();
           model.sliders = _eduHomeDbContext.Slides.Where(x => !x.IsDeleted).ToList();
            
            return View(model);
        }

    }
}