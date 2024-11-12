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

			bool isSuccess = await _userService.LoginUserAsync(model);
			if (isSuccess)
			{
				return RedirectToAction("Index", "Home"); 
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				return View(model);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _userService.LogoutUserAsync();
			TempData["SuccessMessage"] = "You have successfully logged out.";
			return RedirectToAction("Index", "Home"); 
		}
	}
}
