using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using PcBuilder.Data.Models;
using PcBuilder.Services.Data;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Web.Infrastructure.Extensions;
using PcBuilder.Web.Infrastructure.Extentions;
using PcBuilder.Web.Infrastructure.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


        builder.Services
	        .AddDbContext<PCBuilderDbContext>(options =>
            {
	        options.UseSqlServer(connectionString);
	        });

        builder.Services
            .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
                {
                    cfg.Password.RequireDigit =
                        builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
                    cfg.Password.RequireLowercase =
                        builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                    cfg.Password.RequireUppercase =
                        builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                    cfg.Password.RequireNonAlphanumeric =
                        builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
                    cfg.Password.RequiredLength =
                        builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
                    cfg.Password.RequiredUniqueChars =
                        builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");
                })
            .AddEntityFrameworkStores<PCBuilderDbContext>()
            .AddRoles<IdentityRole<Guid>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddDefaultTokenProviders();

		builder.Services.ConfigureApplicationCookie(cfg =>
        {
            cfg.LoginPath = "/User/Login";
            cfg.AccessDeniedPath = "/Home/Error/401";
        });

		builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
		builder.Services.RegisterUserDefinedServices(typeof(IProductService).Assembly);

		builder.Services.AddScoped<IProductService, ProductService>();

		builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();


		var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
			app.UseExceptionHandler("/Home/Error"); 
			app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

			app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.ApplyMigrations();

		using (var scope = app.Services.CreateScope())
		{
			var services = scope.ServiceProvider;
			DataSeeder.SeedRolesAndAdminAsync(services).GetAwaiter().GetResult();
		}

		app.Run();

	   
    }
}








