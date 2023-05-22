using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath,"Libraries")),
    RequestPath = "/lib",
    OnPrepareResponse = ctx => ctx.Context.Response.Headers.Append("Cache-control", "public, max - age = 1000")
});

app.MapGet("/", httpCtx =>
{
    httpCtx.Response.Redirect("/home.html");
    return Task.CompletedTask;
});

app.Run();
