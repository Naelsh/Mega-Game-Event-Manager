using Application.Helpers;
using Application.Models.Activity;
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MegagameEventManager.Test.Unit.Services
{
    public class ActivityGetTest
    {
        WebApplication _app;

        public ActivityGetTest()
        {
            _app = Build();
        }

        [Fact]
        public async void GetAll_NoEntriesInDataBase_ReturnEmptyList()
        {
            int expected = 0;
            var activityService = _app.Services.GetService<IActivityService>();
            var returnValue = await activityService.GetAll();
            Assert.Equal(expected, returnValue.Count());
        }

        [Fact]
        public async void GetAll_EntryInDatabase_ReturnList()
        {
            int expected = 1;

            ActivityPostRequest model = new ActivityPostRequest
            {
                Name = "Event Title",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };

            var activityService = _app.Services.GetService<IActivityService>();
            activityService.Post(model);

            var returnValue = await activityService.GetAll();
            Assert.Equal(expected, returnValue.Count());
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
}
