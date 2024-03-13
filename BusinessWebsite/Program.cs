using BusinessSolutionShared;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ConfigurationBuilder>();

builder.Services.AddScoped<MySqlConnection>();

builder.Services.AddScoped<DbConnector>();

builder.Services.AddScoped<IDbConnection>(s =>
{
    var dbConnector = s.GetRequiredService<DbConnector>();

    return dbConnector.CreateConnection();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error")
       .UseHsts();
}

app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting()
   .UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
