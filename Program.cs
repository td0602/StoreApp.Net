using App.Models;
using Microsoft.EntityFrameworkCore;
using StoreApp.Models;
using StoreApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// cấu hình dịch vụ DbContext
// string connectionString = builder.Configuration.GetConnectionString("QLBanVaLiContext");
builder.Services.AddDbContext<QLBanVaLiContext>();
//cấu hình dịch vụ cho các Repository
builder.Services.AddScoped<ILoaiSpRepository, LoaiSpRepository>();
// đăng ký dịch vụ Session phục vụ login logout
builder.Services.AddSession();
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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}"); // để vào bắt buộc đăng nhập

app.Run();
