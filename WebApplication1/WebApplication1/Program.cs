using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models;
using WebApplication1.Service;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("HopAmChuan");
builder.Services.AddDbContext<HopAmChuan2Context>(options => options.UseSqlServer(connectionString));

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}); 

// Add authentication support
builder.Services.AddAuthentication("MyCookieAuthenticationScheme")
    .AddCookie("MyCookieAuthenticationScheme", options =>
    {
        options.LoginPath = "/Login/Index/";
    });
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<EmailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // BaiViet route
    endpoints.MapControllerRoute(
        name: "BaiViet",
        pattern: "hop-am/{metatitle}.{id?}",
        defaults: new { controller = "BaiViet", action = "ChiTiet" });

    // TimKiem route
    endpoints.MapControllerRoute(
        name: "TimKiem",
        pattern: "hop-am/tim-kiem-hop-am",
        defaults: new { controller = "BaiViet", action = "TimKiem" });

    // Tags route
    endpoints.MapControllerRoute(
        name: "Tags",
        pattern: "Tags/{key}",
        defaults: new { controller = "BaiViet", action = "Tag" });
});

app.Run();
