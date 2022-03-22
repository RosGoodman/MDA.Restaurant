
namespace Common.DAL.Repositories;

/// <summary> CRUD интерфейс репозиториев. </summary>
public interface IRepository<T>
{
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(int id);
    public T GetById(int id);
}
