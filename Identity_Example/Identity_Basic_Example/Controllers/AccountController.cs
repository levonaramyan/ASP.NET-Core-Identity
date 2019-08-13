using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity_Basic_Example.Models;
using Identity_Basic_Example.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Basic_Example.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManage;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManage = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Year = model.Year
                };

                IdentityResult result = await _userManage.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error.Description);
                    }
                }
            }

            return View(model);
        }
    }
}