using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;

namespace MegaGame.Test.Unit;

[TestFixture]
internal class ExampleTest
{
    [Test]
    public void Test()
    {
        var service = new ActivityService();
        Assert.IsTrue(true);
    }
}
