using MySql.Data.MySqlClient;
using System.Data;

// Create a builder to set up and configure our web application.
var builder = WebApplication.CreateBuilder(args);

// Adds MVC services, including controllers, views, and other related services, in the ASP.NET Core Dependency Injection (DI) container.
builder.Services.AddControllersWithViews();

// Build the application using the configuration and services we've set up in the builder.
var app = builder.Build();

// Configure the HTTP request pipeline (Set up how the application handles web requests, including error pages, secure connections, serving files,
// defining routes, handling authorization, and specifying the default route).
// {
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

// Configure the default route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
// }
