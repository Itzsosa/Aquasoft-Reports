using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Aquasoft_Reports.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Aquasoft_Reports.Data.Contexto>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("StringConexion")));

//Se configura el esquema de autenticaci�n por medio de cookies en la aplicaci�n web

builder.Services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication",
        config =>
        {
            config.Cookie.Name = "UserLoginCookie";
            config.LoginPath = "/Usuarios/Login";
        });


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Aquasoft_Reports.Data.Contexto>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("StringConexion")));

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
