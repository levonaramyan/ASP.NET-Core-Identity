using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Email_Confirmation_MVC.Models;
using Email_Confirmation_MVC.Services;
using Email_Confirmation_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Email_Confirmation_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
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
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(
                        model.Email,
                        "Confirm Registration",
                        $"Գրանցումը հաստատելու համար անցեք նշված հղմամբ․ <a href='{callbackUrl}'>հաստատել</a>");

                    return Content("Գրանցումն ավարտին հասցնելու համար ստուգեք ձեր էլ․ փոստն ու հաստատեք գրանցումը։");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return View("Error");


            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Դուք դեռ չեք հաստատել ձեր էլ․ փոտի հասցեն");
                        return View(model);
                    }

                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(model.ReturnUrl))
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(model.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Սխալ մուտքանուն և/կամ գաղտնաբառ");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}