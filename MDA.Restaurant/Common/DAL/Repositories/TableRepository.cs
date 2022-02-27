
using Common.DAL.Models;

namespace Common.DAL.Repositories;

public interface ITableRepository : IRepository<TableModel>
{
    public void BookFreeTable(int countOfPerson);
    public void BookFreeTableAsync(int countOfPerson);
}

public class TableRepository : ITableRepository
{
    public void BookFreeTable(int countOfPerson)
    {
        throw new NotImplementedException();
    }

    public void BookFreeTableAsync(int countOfPerson)
    {
        throw new NotImplementedException();
    }

    public void CreateAsync(TableModel entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TableModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateAsync(TableModel entity)
    {
        throw new NotImplementedException();
    }
}
