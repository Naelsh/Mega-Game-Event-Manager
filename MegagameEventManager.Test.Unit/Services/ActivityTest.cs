using Application.Helpers;
using Application.Models.Activity;
using Application.Services;
using Domain;
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
    public class ActivityTest
    {
        WebApplication _app;

        public ActivityTest()
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
                EndDate = DateTime.Now.AddDays(1),
                Description = "Event description",
                Location = "Event location",
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
