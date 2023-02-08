using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vehicle_Database_MVC.Data;
using Vehicle_Database_MVC.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VehicleDbContext>(options=>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Vehicle_Database_ConnectionString")
        ));

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMappingProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
