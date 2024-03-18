using BusinessWebsite;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("client_inquiries");

    var connection = new MySqlConnection(connectionString);

    connection.Open();

    return connection;
});

builder.Services.AddScoped<IBusinessWebsiteRepository, BusinessWebsiteRepository>();

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
