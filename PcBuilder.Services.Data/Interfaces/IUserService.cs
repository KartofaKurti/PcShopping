using Microsoft.AspNetCore.Identity;
using PcBuilder.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task<IdentityResult> RegisterUserAsync(RegisterFormModel model);
		Task<bool> LoginUserAsync(LoginFormModel model);
		Task LogoutUserAsync();
	}
}
