using BusinessLogic; // This is the namespace where the InquiryRepository class is defined
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database connection and repository
builder.Services.AddScoped<IDbConnection>((s) =>
{
    IDbConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("client_inquiries"));
    conn.Open();
    return conn;
});

builder.Services.AddTransient<InquiryRepository>();

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

// Configure default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
