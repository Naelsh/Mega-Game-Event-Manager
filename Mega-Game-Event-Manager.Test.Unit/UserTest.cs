using Application.Authentication;
using Application.Helpers;
using Application.Models.User;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.DataContexts;
using System.Collections;

namespace Mega_Game_Event_Manager.Test.Unit;

public class UserInvaliedRegisterTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new RegisterRequest { FirstName = "Test", LastName = "Test", Password = "Test" }
        };
        yield return new object[]
        {
            new RegisterRequest { FirstName = "Test", Password = "Test", Username = "Test" }
        };
        yield return new object[]
        {
            new RegisterRequest { LastName = "Test", Password = "Test", Username = "Test" }
        };

    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class UserNoPasswordRegisterTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new RegisterRequest { FirstName = "Test", LastName = "Test", Username = "Test" }
        };

    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class UserTest
{
    WebApplication _app;

    public UserTest()
    {
        _app = Build();
    }

    [Fact]
    public void Register_ValidInput_CreatesNewUser()
    {
        string expected = "test";

        RegisterRequest model = new RegisterRequest
        {
            FirstName = "test",
            LastName = "test",
            Username = "test",
            Password = "test"
        };

        var userService = _app.Services.GetService<IUserService>() ?? null;
        userService.Register(model);

        _app.Services.CreateScope();
        InMemoryDataContext dataContext;
        User user;
        using (var scope = _app.Services.CreateScope())
        {
            dataContext = scope.ServiceProvider.GetRequiredService<InMemoryDataContext>();
            user = dataContext.Users.SingleOrDefault(user => user.FirstName == model.FirstName);
        }

        string actual = user.FirstName;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(UserInvaliedRegisterTestData))]
    public void Register_InvalidInput_ThrowsException(RegisterRequest model)
    {
        var userService = _app.Services.GetService<IUserService>();
        Assert.ThrowsAsync<DbUpdateException>(() => userService.Register(model));
    }

    [Theory]
    [ClassData(typeof(UserNoPasswordRegisterTestData))]
    public void Register_NoPasswordInRequest_ThrowsNullException(RegisterRequest model)
    {
        var userService = _app.Services.GetService<IUserService>();
        Assert.ThrowsAsync<ArgumentNullException>(() => userService.Register(model));
    }

    private static WebApplication Build()
    {
        var builder = WebApplication.CreateBuilder();

        {
            var services = builder.Services;
            services.AddDbContext<DataContext, InMemoryDataContext>();
            services.AddControllers();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
        }

        var app = builder.Build();

        {
            app.UseMiddleware<JwtMiddleware>();
            app.MapControllers();
        }

        return app;
    }
}