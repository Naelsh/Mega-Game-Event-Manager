using Application.Authentication;
using Application.Helpers;
using Application.Services;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

// get the allowed origins from app config file
var allowedOrigions = builder.Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];
services.AddCors(opt =>
    {
        opt.AddPolicy("FrontendInternal", b => b.WithOrigins(allowedOrigions).AllowAnyMethod().AllowAnyHeader());
        opt.AddPolicy("PublicAPI", b => b.AllowAnyOrigin().WithMethods("Get").AllowAnyHeader());
    }
);
services.AddControllers();

services.AddAuthentication("Bearer").AddIdentityServerAuthentication("Bearer", opt => {
    opt.ApiName = "MegagameAPI"; // name of client
    opt.Authority = "https://localhost:7215"; // name of auth server
});

services.AddDbContext<DataContext>();

services.AddAutoMapper(typeof(AutoMapperProfile));

services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

services.AddScoped<IJwtUtils, JwtUtils>();
services.AddScoped<IUserService, UserService>();
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

app.UseRouting();
app.UseCors("FrontendInternal");

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
