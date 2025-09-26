using FoundChildrenGP.BL;
using LostChildrenGP.BL;
using LostChildrenGP.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary");
builder.Services.AddSingleton(new CloudinaryService(
    cloudinaryConfig["CloudName"],
    cloudinaryConfig["ApiKey"],
    cloudinaryConfig["ApiSecret"]
));
builder.Services.AddDbContext<ChildrenContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireDigit = true;
    option.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ChildrenContext>();
builder.Services.AddScoped<ISections, ClsSections>();
builder.Services.AddScoped<IWorkSteps, ClsWorkSteps>();
builder.Services.AddScoped<IFAQ, ClsFAQ>();
builder.Services.AddScoped<IKPI, ClsKPI>();
builder.Services.AddScoped<ISlider, ClsSlider>();
builder.Services.AddScoped<ILostChildren, ClsLostChildren>();
builder.Services.AddScoped<IFoundChildren, ClsFoundChildren>();
builder.Services.AddScoped<ISearchResult, ClsSearchResult>();
builder.Services.AddScoped<IResultChildren, ClsResultChildren>();
builder.Services.AddScoped<IHistory, ClsHistory>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Error/E403";
    option.Cookie.Name = "Cookie";
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromMinutes(720);
    option.LoginPath = "/User/LoginForm";
    option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    option.SlidingExpiration = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseSession();



app.UseEndpoints(endpoints =>
{
    
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});

app.Run();
