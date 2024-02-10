using InquiryApp;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DbConnector>();

builder.Services.AddScoped<IDbConnection>(s => DbConnector.CreateConnection());

builder.Services.AddScoped<InquiryRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Inquiry/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inquiry}/{action=index}/{id?}");

app.Run();
