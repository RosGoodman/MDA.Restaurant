
using Common.DAL.Context;
using Common.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.DAL.Repositories;

/// <summary> Интерфейс для контейнера. </summary>
public interface ITableRepository : IRepository<TableModel>
{
    /// <summary> Обновить данные экземпляра. </summary>
    /// <param name="entity"> Экземпляр класса с новыми данными. </param>
    public void Update(TableModel entity);
    public Task<int> BookingTableAsync(int seatsCount);
    public int BookingTable(int seatsCount);
}

/// <summary> Репозиторий TableModel. </summary>
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

    /// <summary> Забронировать столик. Асинхронная операция. </summary>
    /// <param name="seatsCount"> Минимальное кол-во мест. </param>
    /// <returns> Номер забронированного столика или -1. </returns>
    public int BookingTable(int seatsCount)
    {
        try
        {
            using var transaction = _context.ContextBeginTransaction();
            var table = _context.Tables.Where(t => t.State == 0 && t.SeatsCount >= seatsCount).FirstOrDefault();
            Thread.Sleep(5000);
            if (table is null || table.State == Enums.State.Booked) return -1;

            //обновление состояния стола в БД.
            table.State = Enums.State.Booked;
            UpdateAsync(table);
            transaction.Commit();

            return table.Id;
        }
        catch (Exception ex) { _logger.LogError(ex, "ошибка при попытке бронирования столика."); }
        return -1;
    }

    /// <summary> Забронировать столик. Асинхронная операция. </summary>
    /// <param name="seatsCount"> Минимальное кол-во мест. </param>
    /// <returns> Номер забронированного столика или -1. </returns>
    public async Task<int> BookingTableAsync(int seatsCount)
    {
        try
        {
            using var transaction = _context.ContextBeginTransaction();

            var table = await _context.Tables.Where(t => t.State == 0 && t.SeatsCount >= seatsCount).FirstOrDefaultAsync();
            Thread.Sleep(5000);
            if (table is null || table.State == Enums.State.Booked) return -1;

            //обновление состояния стола в БД.
            table.State = Enums.State.Booked;
            UpdateAsync(table);
            transaction.Commit();

            return table.Id;
        }
        catch (Exception ex) { _logger.LogError(ex, "ошибка при попытке бронирования столика."); }
        return -1;
    }

    /// <summary> Записать экземпляр в БД. </summary>
    /// <param name="entity"> Записываемый ресторан. </param>
    public async void CreateAsync(TableModel entity)
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
    public async Task<TableModel> GetByIdAsync(int id)
    {
        try
        {
            var table = await _context.Tables.Where(x => x.Id == id).FirstOrDefaultAsync();
            return table;
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
