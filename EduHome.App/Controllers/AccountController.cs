using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

      public  async Task<IActionResult> Register()
        {
            return View();
        }
        //public async Task<IActionResult> CreatRole()
        //{
        //    //IdentityRole identityRole1 = new IdentityRole { Name = "SuperAdmin" };
        //    //IdentityRole identityRole2 = new IdentityRole { Name = "Admin" };
        //    //IdentityRole identityRole3 = new IdentityRole { Name = "User" };

        //    //await _roleManager.CreateAsync(identityRole1);
        //    //await _roleManager.CreateAsync(identityRole2);
        //    //await _roleManager.CreateAsync(identityRole3);
        //    //return Json("ok");
        //}
    }
}
