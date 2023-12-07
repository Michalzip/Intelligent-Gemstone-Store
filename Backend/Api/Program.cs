using IntelligentStore.ServiceInjector.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddService();
var app = builder.Build();
app.UseSession();

app.UseService();
app.MapControllers();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
