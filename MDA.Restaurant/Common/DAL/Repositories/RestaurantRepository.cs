
using Common.DAL.Context;
using Common.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.DAL.Repositories;

/// <summary> Интерфейс для контейнера. </summary>
public interface IRestaurantRepository : IRepository<RestaurantModel> { }

/// <summary> Репозиторий модели ресторана. </summary>
public class RestaurantRepository : IRestaurantRepository
{
    private readonly ILogger _logger;
    private readonly IContextDB _context;

    /// <summary> Конструктор класса. </summary>
    /// <param name="context"> Контекст БД. </param>
    /// <param name="logger"> Логгер. </param>
    public RestaurantRepository(IContextDB context, ILogger<RestaurantRepository> logger)
    {
        _context = context;
        _logger = logger;
        _logger.LogDebug(1, "Логгер встроен в RestaurantRepository");
    }

    /// <summary> Записать экземпляр в БД. </summary>
    /// <param name="entity"> Записываемый ресторан. </param>
    public void Create(RestaurantModel entity)
    {
        try
        {
            _context.Restaurants.Add(entity);
            _context.ContextSaveChanges();
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке записать экземпляр ресторана в БД."); }
    }

    /// <summary> Удалить по ID. </summary>
    /// <param name="id"> ID ресторана </param>
    public void Delete(int id)
    {
        try
        {
            var restaurant = _context.Restaurants.Where(x => x.Id == id).FirstOrDefault();
            if (restaurant is null) return;

            _context.Restaurants.Remove(restaurant);
            _context.ContextSaveChanges();
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке удалить экземпляр ресторана из БД."); }
    }

    /// <summary> Получить экземпляр ресторана по ID. </summary>
    /// <param name="id"> ID ресторана. </param>
    /// <returns> Полученный экземпляр. </returns>
    public RestaurantModel GetById(int id)
    {
        try
        {
            var rest = _context.Restaurants.Where(x => x.Id == id).FirstOrDefault();
            return rest;
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке получить экземпляр ресторана из БД."); }
        return null;
    }

    /// <summary> Обновить данные ресторана в БД. </summary>
    /// <param name="entity"> Экземпляр с новыми данными. </param>
    public void Update(RestaurantModel entity)
    {
        try
        {
            var restaurant = _context.Restaurants.Where(x => x.Name == entity.Name).FirstOrDefault();
            if(restaurant is null) return;

            restaurant.Name = entity.Name;
            _context.Restaurants.Update(restaurant);
            _context.ContextEntryModified(restaurant);
            _context.ContextSaveChanges();
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке обновить экземпляр ресторана в БД."); }
    }
}
