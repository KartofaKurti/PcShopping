using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;

namespace PcBuilder.Web.Infrastructure.Extentions
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
		{
			using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

			PCBuilderDbContext dbContext = serviceScope
				.ServiceProvider
				.GetRequiredService<PCBuilderDbContext>()!;
			dbContext.Database.Migrate();

			return app;
		}
	}
}
