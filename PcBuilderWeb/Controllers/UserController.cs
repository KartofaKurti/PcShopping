using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Data.Models;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Web.ViewModels.User;

namespace PcBuilderWeb.Controllers
{
	public class UserController : BaseController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterFormModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await _userService.RegisterUserAsync(model);
                if (result.Succeeded)
                {
                    await _userService.LoginUserAsync(new LoginFormModel { Email = model.Email, Password = model.Password });
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred during registration. Please try again later.");
                Console.WriteLine($"Error: {ex.Message}");
            }

            return View(model);
        }

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginFormModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                bool isSuccess = await _userService.LoginUserAsync(model);
                if (isSuccess)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred during login. Please try again later.");
                Console.WriteLine($"Error: {ex.Message}");
            }

            return View(model);
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
            try
            {
                await _userService.LogoutUserAsync();
                TempData["SuccessMessage"] = "You have successfully logged out.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred during logout.";
                Console.WriteLine($"Error: {ex.Message}");
            }

            return RedirectToAction("Index", "Home");
        }
	}
}
