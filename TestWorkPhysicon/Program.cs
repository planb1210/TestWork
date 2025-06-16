using TestWorkPhysicon.DataBase;
using TestWorkPhysicon.DataBase.Repositories;
using TestWorkPhysicon.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDBContext, DBContext>();
builder.Services.AddTransient<IModulesRepository, ModulesRepository>();
builder.Services.AddTransient<ICoursesRepository, CoursesRepository>();
builder.Services.AddTransient<ILibraryService, LibraryService>();

builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
