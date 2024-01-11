using InquiryApp;
using MySql.Data.MySqlClient;
using System.Data;

// Create a builder to set up and configure our web application.
var builder = WebApplication.CreateBuilder(args);

// Register controllers, views, and related services in the ASP.NET Core Dependency Injection (DI) container.
builder.Services.AddControllersWithViews();

// Get the folder where the application is running (the base directory).
var baseDirectory = AppContext.BaseDirectory;

// Combine the base directory with the file name to create the full path to appsettings.json. This full path is used to locate and access the
// configuration settings, including the connection string that enables the program to establish a connection with the database.
var appSettingsPath = Path.Combine(baseDirectory, "appsettings.json");

/*
Configure the database connection as a scoped service in the ASP.NET Core dependency injection (DI) container. This allows the configured and open 
database connection to be injected into components (controllers, services, etc.) whenever an IDbConnection is requested. The connection is scoped to 
the lifetime of an HTTP request and will be disposed of at the end of the request.
*/
builder.Services.AddScoped<IDbConnection>((s) =>
{
    // Use the explicitly specified path to appsettings.json to build the configuration.
    var configuration = new ConfigurationBuilder().AddJsonFile(appSettingsPath).Build();

    // Create a new instance of MySqlConnection using the configured connection string.
    IDbConnection conn = new MySqlConnection(configuration.GetConnectionString("client_inquiries"));

    // Open the database connection.
    conn.Open();

    // Return the configured and open database connection.
    return conn;
});

/*
Register InquiryRepository as a scoped service in the ASP.NET Core dependency injection (DI) container. This ensures that a new instance of 
InquiryRepository is created for each HTTP request, promoting a scoped behavior aligned with the lifespan of the associated database connection. 
This registration allows the injection of InquiryRepository instances when requested.
*/
builder.Services.AddScoped<InquiryRepository>();

// Build the application using the configuration and services we've set up in the builder.
var app = builder.Build();

// Configure the HTTP request pipeline (Set up how the application handles web requests, including error pages, secure connections, serving files,
// defining routes, handling authorization, and specifying the default route).
// {
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Inquiry/Error");

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
    pattern: "{controller=Inquiry}/{action=index}/{id?}");

app.Run();
// }