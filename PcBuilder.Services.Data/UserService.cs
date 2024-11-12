using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PcBuilder.Data.Models;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Web.ViewModels.User;

namespace PcBuilder.Services.Data
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;

		public UserService(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole<Guid>> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}
		public async Task<IdentityResult> RegisterUserAsync(RegisterFormModel model)
		{
			var user = new ApplicationUser
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName
			};

			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				if (!await _roleManager.RoleExistsAsync("USER"))
				{
					await _roleManager.CreateAsync(new IdentityRole<Guid>("USER"));
				}

				await _userManager.AddToRoleAsync(user, "USER");
			}

			return result;
		}

		public async Task<bool> LoginUserAsync(LoginFormModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
			return result.Succeeded;
		}

		public async Task LogoutUserAsync()
		{
			await _signInManager.SignOutAsync();
		}
	}
}
