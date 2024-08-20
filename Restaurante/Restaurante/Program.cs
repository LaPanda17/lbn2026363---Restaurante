using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurante.Models;
using Microsoft.AspNetCore.Authentication.Cookies;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion bd
builder.Services.AddDbContext<RestauranteContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("RestauranteContext") ?? throw new InvalidOperationException("Connection string 'RestauranteContext' not found.")));


// Configuración del contexto de datos
/*builder.Services.AddDbContext<RestauranteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestauranteContext") ?? throw new InvalidOperationException("Connection string 'RestauranteContext' not found.")));

*/
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
    option =>
    {
        //Formulario de logeo
        option.LoginPath = "/Accesos/Index";
        //Tiempo de acceso
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        //Auteticacion denegada
        option.AccessDeniedPath = "/Home/Privacy";
    }
    );

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
app.UseAuthentication();  // Añade esta línea


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
