using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiwiSalud.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/* Configuracion de las cookies */
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(option =>{
    option.LoginPath = "/Usuarios/Index";
    option.ExpireTimeSpan = TimeSpan.FromSeconds(20.0);
    option.AccessDeniedPath = "/Usuarios/Index";
});

builder.Services.AddDbContext<BaseContext>(options =>
        options.UseMySql(
            builder.Configuration.GetConnectionString("MySqlConnection"),
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")
        ));


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

app.UseAuthorization();

/* Uso de authentication para el guardian */
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

app.Run();
