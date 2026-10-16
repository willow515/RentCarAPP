using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Session;
using PRojectRentcarwebclident.Services;
using PRojectRentcarwebclident.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ApiService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseSession();        
app.UseAuthentication(); 
app.UseAuthorization();  

app.MapRazorPages();

app.Run();