using IntelligentStore.ServiceInjector.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddService();
var app = builder.Build();
app.UseSession();
app.UseService();
app.Run();
