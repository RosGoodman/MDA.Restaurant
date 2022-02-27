using Common.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.DAL.Context
{
    public interface IContextDB
    {
        DbSet<RestaurantModel> Restaurants { get; set; }
        DbSet<TableModel> Tables { get; set; }

        public void ContextSaveChanges();
        public void ContextEntryModified(object entity);
    }
}