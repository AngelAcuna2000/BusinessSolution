using LARemodeling;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// No need to set the base path when appsettings.json is inside the project

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("client_inquiries");

    var connection = new MySqlConnection(connectionString);

    connection.Open();

    return connection;
});

builder.Services.AddScoped<IDapperWrapper, DapperWrapper>();

builder.Services.AddScoped<ILARemodelingRepo, LARemodelingRepo>();

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
