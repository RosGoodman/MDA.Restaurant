
using Common.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Common.DAL.Context;

public class ContextDB : DbContext
{
    public DbSet<TableModel> Tables { get; set; }
    public DbSet<Restaurant> Restaurants { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));
    }
}
