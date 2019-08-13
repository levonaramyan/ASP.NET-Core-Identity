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
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Year = model.Year
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                } else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Year = user.Year
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);

                if(user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    } else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

    }
}