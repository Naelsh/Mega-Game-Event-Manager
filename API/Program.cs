using Application.Helpers;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var env = builder.Environment;

// use sql server db in production and sqlite db in development
//if (env.IsProduction())
    services.AddDbContext<DataContext>();
//else
//    services.AddDbContext<DataContext, SqliteDataContext>();

services.AddCors();
services.AddControllers();

services.AddAutoMapper(typeof(AutoMapperProfile));
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

services.AddScoped<IActivityService, ActivityService>();

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
