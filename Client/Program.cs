var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(
        config =>
        {
            //check the cookie to confirm that we are authenticated
            config.DefaultAuthenticateScheme = "ClientCookie";
            // 
            config.DefaultSignInScheme = "ClientCookie";
            
            config.DefaultChallengeScheme = "OurServer";


        })
    .AddCookie("ClientCookie")
    .AddOAuth("OurServer", config =>
    {
        config.CallbackPath = "/oauth/callback/";
        config.ClientId = "client";
        config.ClientSecret = "secret";
        config.AuthorizationEndpoint = "https//localhost:44333/oauth/login";
        config.TokenEndpoint = "https//localhost:44333/oauth/token";
    });


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
    endpoints.MapDefaultControllerRoute();
});

app.Run();
