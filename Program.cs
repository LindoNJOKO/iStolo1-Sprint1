using iStolo1.Controllers;
using iStolo1.Data;
using iStolo1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using FirebaseAdmin;
//using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Firebase Configuration
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("C:\\Users\\lindo\\source\\repos\\iStolo1\\istolo-f1a0d-firebase-adminsdk-pwuwv-24e0813f06.json")
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/your-firebase-project-id";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/your-firebase-project-id",
            ValidateAudience = true,
            ValidAudience = "istolo-f1a0d\r\n",
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<iStolo1DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<iStolo1DbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Configure your identity options
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
});

// Add the AccountController to the MVC framework
builder.Services.AddControllersWithViews()
    .AddControllersAsServices()
    .AddApplicationPart(typeof(AccountController).Assembly);

// Add logging services
builder.Services.AddLogging();

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

app.UseAuthentication(); // Add this line for authentication
app.UseAuthorization();

// Adding certificate validation callback (for testing/debugging only)
app.Use(async (context, next) =>
{
    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Main}/{id?}");

app.Run();
