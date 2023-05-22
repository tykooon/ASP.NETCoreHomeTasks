using Company.Core.Interfaces;
using Company.Infrastructure;
using Company.SqliteDb;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.Configure<UnitOfWorkOptions>(
    builder.Configuration.GetSection("UnitOfWorkOptions"));
using (var db = new DbProvider(
    builder.Configuration.GetValue<string>("DbProviderDataFile")))
{
    db.PrepareDb();
}
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.MapRazorPages();
app.MapControllers();

app.Run();
