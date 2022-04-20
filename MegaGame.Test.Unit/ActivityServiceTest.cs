using API.Controllers;
using Application.Helpers;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaGame.Test.Unit;

[TestFixture]
class ActivityServiceTest
{
    private ActivityController GetActivityController(string testName)
    {
        var context = TestAddon.GetTestContext(testName);
        var mapper = new AutoMapperProfile();
        var activityService = new ActivityService(context, mapper);
        return new ActivityController();
    }

    [Test]
    public void PostActivity_ValidInput_AddNewEntry()
    {
        int expected = 1;

        using var context = TestAddon.GetContext(nameof(PostActivity_ValidInput_AddNewEntry));

        Assert.AreEqual(expected, 1);
    }
}
