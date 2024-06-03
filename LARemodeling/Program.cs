using LARemodeling;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<MySqlConnection>((s) =>
{
    MySqlConnection conn = new(builder.Configuration.GetConnectionString("azure"));
    conn.Open();
    return conn;
});

builder.Services.AddTransient<ILARemodelingRepo, LARemodelingRepo>();

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
