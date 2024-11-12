using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PcBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.Infrastructure.Services
{
	public class DataSeeder
	{
		public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
		{
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
			var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

			string[] roleNames = { "Admin", "User" };

			foreach (var roleName in roleNames)
			{
				if (!await roleManager.RoleExistsAsync(roleName))
				{
					await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
				}
			}

			string adminEmail = "admin@gmail.com";
			string adminPassword = "admin";

			var adminUser = await userManager.FindByEmailAsync(adminEmail);
			if (adminUser == null)
			{
				var identityOptions = services.GetRequiredService<IOptions<IdentityOptions>>();
				var originalOptions = identityOptions.Value;

				identityOptions.Value.Password.RequireDigit = false;
				identityOptions.Value.Password.RequireLowercase = false;
				identityOptions.Value.Password.RequireUppercase = false;
				identityOptions.Value.Password.RequireNonAlphanumeric = false;
				identityOptions.Value.Password.RequiredLength = 1;

				adminUser = new ApplicationUser
				{
					FirstName = "ADMIN",
					LastName = "ADMIN",
					UserName = adminEmail,
					Email = adminEmail,
					EmailConfirmed = true
				};

				var result = await userManager.CreateAsync(adminUser, adminPassword);

				identityOptions.Value.Password = originalOptions.Password;

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(adminUser, "Admin");
				}
				else
				{
					throw new Exception("Failed to create admin user: " +
					                    string.Join(", ", result.Errors.Select(e => e.Description)));
				}
			}
		}
	}
}