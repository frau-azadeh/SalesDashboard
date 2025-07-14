using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SalesDashboard.Models; // محل DbContext شما

var builder = WebApplication.CreateBuilder(args);

// 🔧 اتصال به دیتابیس SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// اضافه کردن MVC و سشن
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// 🔐 میدلورها
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ⚠️ قبل از Authorization
app.UseAuthorization();

// 🔗 مسیر پیش‌فرض به صفحه لاگین
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
