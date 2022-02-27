
using Common.DAL.Context;
using Common.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.DAL.Repositories;

public interface ITableRepository : IRepository<TableModel>
{
    public void Update(TableModel entity);
}

public class TableRepository : ITableRepository
{
    private readonly ILogger _logger;
    private readonly IContextDB _context;

    /// <summary> Конструктор класса. </summary>
    /// <param name="context"> Контекст БД. </param>
    /// <param name="logger"> Логгер. </param>
    public TableRepository(IContextDB context, ILogger<TableRepository> logger)
    {
        _context = context;
        _logger = logger;
        _logger.LogDebug(1, "Логгер встроен в TableRepository");
    }

    /// <summary> Записать экземпляр в БД. </summary>
    /// <param name="entity"> Записываемый ресторан. </param>
    public async void CreateAsync(TableModel  entity)
    {
        try
        {
            await _context.Tables.AddAsync(entity);
            _context.ContextSaveChanges();
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке записать экземпляр стола в БД."); }
    }

    /// <summary> Удалить по ID. </summary>
    /// <param name="id"> ID стола </param>
    public async void DeleteAsync(int id)
    {
        try
        {
            var tableModel = await _context.Tables.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (tableModel is null) return;

            _context.Tables.Remove(tableModel);
            _context.ContextSaveChanges();
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке удалить экземпляр стола из БД."); }
    }

    /// <summary> Получить экземпляр стола по ID. </summary>
    /// <param name="id"> ID стола. </param>
    /// <returns> Полученный экземпляр. </returns>
    public async Task<TableModel > GetByIdAsync(int id)
    {
        try
        {
            return await _context.Tables.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке получить экземпляр стола из БД."); }
        return null;
    }

    /// <summary> Обновить данные стола в БД. </summary>
    /// <param name="entity"> Экземпляр с новыми данными. </param>
    public void Update(TableModel entity)
    {
        try
        {
            var tableModel = _context.Tables.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (tableModel is null) return;

            tableModel.State = entity.State;
            tableModel.SeatsCount = entity.SeatsCount;

            _context.Tables.Update(tableModel);
            _context.ContextEntryModified(tableModel);
            _context.ContextSaveChanges();
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке обновить экземпляр стола в БД."); }
    }

    /// <summary> Обновить данные стола в БД. Асинхронно. </summary>
    /// <param name="entity"> Экземпляр с новыми данными. </param>
    public async void UpdateAsync(TableModel entity)
    {
        try
        {
            var tableModel = await _context.Tables.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            if (tableModel is null) return;

            tableModel.State = entity.State;
            tableModel.SeatsCount = entity.SeatsCount;

            _context.Tables.Update(tableModel);
            _context.ContextEntryModified(tableModel);
            _context.ContextSaveChanges();
        }
        catch (Exception ex) { _logger.LogError(ex, "Ошибка при попытке асинхронно обновить экземпляр стола в БД."); }
    }
}
