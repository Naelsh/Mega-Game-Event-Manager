using Application.Helpers;
using Application.Services;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddCors();
services.AddControllers();

services.AddAutoMapper(typeof(AutoMapperProfile));
services.AddDbContext<DataContext>();

services.AddScoped<IActivityService, ActivityService>();
services.AddScoped<IFactionService, FactionService>();
services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//app.UseAuthorization();

app.MapControllers();

app.Run();
