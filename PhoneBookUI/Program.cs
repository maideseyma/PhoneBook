using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using PhoneBookBusinessLayer.EmailSenderBusiness;
using PhoneBookBusinessLayer.ImplementationsOfManagers;
using PhoneBookBusinessLayer.InterfacesOfManagers;
using PhoneBookDataLayer;
using PhoneBookDataLayer.ImplementationsOfRepo;
using PhoneBookDataLayer.InterfacesOfRepo;
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

//interfacelerin iþlerini gerçekleþtirecek classlarý burada yaþam döngülerini tanýmlamalýyýz.
builder.Services.AddScoped<IMemberManager, MemberManager>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles(); // wwwroot klasörünü görmesi için

app.UseRouting(); // browserdaki url için home/indexe gidebilmesi için

app.UseAuthorization(); // yetkilendirme için

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // routedefault pattern vermek için

app.Run(); // uygulamayý çalýþtýrýr
