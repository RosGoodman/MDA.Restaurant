
namespace Common.DAL.Repositories;

/// <summary> CRUD интерфейс репозиториев. </summary>
public interface IRepository<T>
{
    public void CreateAsync(T entity);
    public void UpdateAsync(T entity);
    public void DeleteAsync(int id);
    public Task<T> GetByIdAsync(int id);
}
