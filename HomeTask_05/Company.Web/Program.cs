using Company.Core.Interfaces;
using Company.Infrastructure;
using Company.SqliteDb;
using Company.Web.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllersWithViews();
builder.Services.Configure<UnitOfWorkOptions>(builder.Configuration.GetSection("UnitOfWorkOptions"));
using (var db = new DbProvider(builder.Configuration.GetValue<string>("DbProviderDataFile")))
{
    db.PrepareDb();
}

builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/login";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.Events.OnValidatePrincipal = PrincipalValidator.ValidateAsync;
        });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LoggedInUser", policy => policy.RequireAuthenticatedUser());
    options.AddPolicy("AdminDepartmentUser", policy => policy.AddRequirements(new DepartmentReq("Administration")));
    options.AddPolicy("AdminRole", policy => policy.RequireRole("Admin"));
});

builder.Services.AddSingleton<IAuthorizationHandler, DepartmentReqHandler>();



var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();
app.Run();
