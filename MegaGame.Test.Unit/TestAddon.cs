using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaGame.Test.Unit;

public class TestAddon
{
    internal static DataContext GetTestContext(string testName)
    {
        var builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase(testName);
        return new DataContext(builder.Options);
    }
}
