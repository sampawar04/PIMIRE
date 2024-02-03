using PimireWebApp.BusinessLogicLayer;
using PimireWebApp.DataAccessLayer;
using PimireWebApp.Models;
using PimireWebApp.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<PimireDbContext>();
builder.Services.AddScoped<ResponseFilterAttribute>();
builder.Services.AddTransient<IProduct, ProductDAL>();
builder.Services.AddTransient<IShop, ShopDAL>();
builder.Services.AddTransient<IEmailHelper, EmailHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shop}/{action=product-list}/{id?}");

app.Run();
