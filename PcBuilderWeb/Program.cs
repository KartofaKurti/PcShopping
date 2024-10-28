using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var testAreaConnString =
    builder.Configuration.GetConnectionString("ServiceAreaConnection") ?? throw new InvalidOperationException("Connection string 'ServiceAreaConnection' not found.");

builder.Services.AddDbContext<PcBuilderDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<PcBuilderDbContext>(options =>
    options.UseSqlServer(testAreaConnString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.Run();
