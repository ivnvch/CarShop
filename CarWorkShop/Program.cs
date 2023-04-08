using CarWorkShop.DAL;
using CarWorkShop.DAL.Interfaces;
using CarWorkShop.DAL.Repositories;
using CarWorkShop.Models.Entity;
using CarWorkShop.Service.Implementations;
using CarWorkShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IBaseRepository<Owner>, OwnerRepository>();
builder.Services.AddScoped<IBaseRepository<Record>, RecordRepository>();
builder.Services.AddScoped<IRecordService, RecordService>();


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