using Application.Helpers;
using Application.Models.Activity;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MegagameEventManager.Test.Unit.Services;

public class ActivityPostTest
{
    WebApplication _app;

    public ActivityPostTest()
    {
        _app = Build();
    }

    [Fact]
    public void Post_ValidInput_NewActivityCreated()
    {
        string expected = "Event Title";
        int expectedAmount = 1;

        ActivityPostRequest model = new ActivityPostRequest
        {
            Name = "Event Title",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        var activityService = _app.Services.GetService<IActivityService>();
        activityService.Post(model);

        Activity activity;
        int actualAmount = 0;
        using (var scope = _app.Services.CreateScope())
        {
            InMemoryDataContext dataContext = scope.ServiceProvider.GetService<InMemoryDataContext>();
            activity = dataContext.Activities.SingleOrDefault(a => a.Id == 1);
            actualAmount = dataContext.Activities.Count();
        }

        string actual = activity.Name;

        Assert.Equal(expected, actual);
        Assert.Equal(expectedAmount, actualAmount);
    }

    [Fact]
    public void Post_EndDateBeforeStartDate_ThrowException()
    {
        ActivityPostRequest model = new ActivityPostRequest
        {
            Name = "Event Title",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now
        };

        var activityService = _app.Services.GetService<IActivityService>();
        Assert.Throws<AppException>(() => activityService.Post(model));
    }

    [Theory]
    [ClassData(typeof(ActivityInvaliedRegisterTestData))]
    public void Post_InvalidInput_ThrowException(ActivityPostRequest model)
    {
        var activityService = _app.Services.GetService<IActivityService>();
        Assert.Throws<AppException>(() => activityService.Post(model));
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
            services.AddScoped<IActivityService, ActivityService>();
        }

        var app = builder.Build();

        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.MapControllers();
        }

        return app;
    }
}

public class ActivityInvaliedRegisterTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new ActivityPostRequest
            {
                Name = "Event Title",
                StartDate = DateTime.Now,
            }
        };
        yield return new object[]
        {
            new ActivityPostRequest
            {
                Name = "Event Title",
                EndDate = DateTime.Now.AddDays(1),
            }
        };
        yield return new object[]
        {
            new ActivityPostRequest
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}