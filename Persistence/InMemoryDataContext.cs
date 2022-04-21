using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public class InMemoryDataContext : DataContext
{
    private readonly string _databaseName;

    public InMemoryDataContext(IConfiguration configuration) : base(configuration)
    {
        _databaseName = "test";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseInMemoryDatabase(_databaseName);
    }
}
