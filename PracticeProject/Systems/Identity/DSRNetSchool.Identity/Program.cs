using DSRNetSchool.Identity;
using DSRNetSchool.Identity.Configuration;
using DSRNetSchool.Settings;

// Configure application
var builder = WebApplication.CreateBuilder(args);

var settings = new IS4Settings(new SettingsSource());

// Configure services
var services = builder.Services;

services.AddAppCors();
services.AddAppDbContext(settings.Db);
services.AddHttpContextAccessor();
services.AddAppServices();
services.AddIS4();

// Start application
var app = builder.Build();

app.UseAppCors();
app.UseStaticFiles();
app.UseRouting();
app.UseAppDbContext();
app.UseIS4();

app.Run();