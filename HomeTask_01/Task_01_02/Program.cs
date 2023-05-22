using Task_01.Controllers.Constraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opt => {
    opt.ConstraintMap.Add("nameTemplate", typeof(NameConstraint)); 
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();

// ------------------------
//endpoints.MapControllerRoute(
//    name: "person",
//    pattern: "{controller=Person}/{name}/{age:min(1):max(120)}",
//    defaults: new { action = "ShowInfo" },
//    constraints: new { name = new RegexRouteConstraint(@"^([A-Za-z]+)(['|-]?[A-Za-z]+)*$") },
//    dataTokens: null);

//endpoints.MapControllerRoute(
//    name: "person-default",
//    pattern: "person/{**catch}",
//    defaults: new
//    {
//        controller = "Person",
//        action = "Index"
//    });

