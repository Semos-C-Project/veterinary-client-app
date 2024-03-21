using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using veterinary_client_app.Data;
using veterinary_client_app.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Connect the Sqlite database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Auth/Login";
    options.LogoutPath = $"/Auth/Logout";
    options.AccessDeniedPath = $"/Auth/AccessDenied";
});



builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter());
} );

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