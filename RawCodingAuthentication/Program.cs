using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using RawCodingAuthentication.AuthorizationRequirement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication( options =>
    {
        options.DefaultScheme = "CookieAuth";
    })
    .AddCookie("CookieAuth",options =>
    {
        options.Cookie.Name = "TestAuth";
        options.LoginPath = "/Home/Authentication";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });


builder.Services.AddAuthorization(c =>
{
    c.AddPolicy("DoB", policyBuilder =>
    {
        policyBuilder.AddRequirements(new CustomRequiredClaims(ClaimTypes.Actor));
    });
});

builder.Services.AddScoped<IAuthorizationHandler,CustomRequiredClaimHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
