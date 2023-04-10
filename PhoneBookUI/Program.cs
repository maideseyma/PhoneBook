using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using PhoneBookDataLayer;
using PhoneBookEntityLayer.Mappings;

var builder = WebApplication.CreateBuilder(args);


// context bilgisi eklenir

builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});
builder.Services.AddAutoMapper(x =>
{
    //x.AddExpressionMapping();
    x.AddProfile(typeof(Maps)); 

});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles(); // wwwroot klas�r�n� g�rmesi i�in

app.UseRouting(); // browserdaki url i�in home/indexe gidebilmesi i�in

app.UseAuthorization(); // yetkilendirme i�in

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // routedefault pattern vermek i�in

app.Run(); // uygulamay� �al��t�r�r
