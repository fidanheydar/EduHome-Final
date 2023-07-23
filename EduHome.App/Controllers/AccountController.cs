using EduHome.App.ViewModels;
using EduHome.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(RegisterViewModel registerView)
        {
            if (!ModelState.IsValid)
            {
                return View(registerView);
            }
            AppUser appUser = new AppUser
            {

                Name = registerView.Name,
                Surname = registerView.Surname,
                UserName = registerView.UserName,
                Email = registerView.Email

            };
          IdentityResult identityResult=  await _userManager.CreateAsync(appUser, registerView.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View(registerView);
            }
            await _userManager.AddToRoleAsync(appUser, "User");
            return RedirectToAction("index", "home"); 
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login )
        {
            AppUser appUser = await _userManager.FindByNameAsync(login.Username);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(login);
            }
            Microsoft.AspNetCore.Identity.SignInResult result = 
                await _signInManager.PasswordSignInAsync(appUser,login.Password,login.RememberMe,true);
           

            if (!result.Succeeded)
            {
                if(result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is blocked for 5 min");
                    return View(login);
                }
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(login);
            }
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        //public async Task<IActionResult> Creatrole() 
        //{
        //    IdentityRole identityRole1 = new IdentityRole { Name = "SuperAdmin" };
        //    IdentityRole identityRole2 = new IdentityRole { Name = "Admin" };
        //    IdentityRole identityRole3 = new IdentityRole { Name = "User" };

        //    await _roleManager.CreateAsync(identityRole1);
        //    await _roleManager.CreateAsync(identityRole2);
        //    await _roleManager.CreateAsync(identityRole3);
        //    return Json("ok");
        //}
    }
}

