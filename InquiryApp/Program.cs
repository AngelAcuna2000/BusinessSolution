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
    app.UseExceptionHandler("/Inquiry/Error")
       .UseHsts();
}

app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting()
   .UseAuthorization();

app.MapControllerRoute("default", "{controller=Inquiry}/{action=Index}/{id?}");

app.Run();
