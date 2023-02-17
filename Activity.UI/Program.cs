using Employee.Service.IOC;
using WaitLess.Service.Common.HTTPClientFactory;
using WatchDog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Activity.UI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ActivityUIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ActivityUIContext") ?? throw new InvalidOperationException("Connection string 'ActivityUIContext' not found.")));


var configbuilder = new ConfigurationBuilder()
      //.SetBasePath("path here") //<--You would need to set the path
      .AddJsonFile("appsettings.json");

IConfiguration Configuration = configbuilder.Build();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Configuration["LocationAPI:URI"])});
builder.Services.AddScoped(sp => new HttpClient());

builder.Services.AddSingleton<IEmployeeHttpClientFactory, EmployeeHttpClientFactory>();
builder.Services.AddHttpClient("EmployeeHttpClientFactory");

builder.Services.RegisterApiServices();

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



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");


app.Run();
