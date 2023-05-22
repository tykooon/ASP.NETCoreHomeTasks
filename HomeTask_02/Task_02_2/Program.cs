using Task_02_2.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IContactRepo, ContactJsonRepo>();
builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToController(action:"PageNotFound", controller:"Contact");

app.Run();