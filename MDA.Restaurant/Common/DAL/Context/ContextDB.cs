
using Common.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Common.DAL.Context;

public class ContextDB : DbContext, IContextDB
{
    public DbSet<TableModel> Tables { get; set; }
    public DbSet<RestaurantModel> Restaurants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));
    }

    public void ContextSaveChanges() => SaveChanges();
    public void ContextEntryModified(object entity) => Entry(entity).State = EntityState.Modified;
    public IDbContextTransaction ContextBeginTransaction() => Database.BeginTransaction();
}
