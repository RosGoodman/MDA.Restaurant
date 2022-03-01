using Common.DAL.Models;
using Common.DAL.Repositories;
using MDA.Restaurant.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDA.Restaurant.Controllers;

/// <summary> Контроллер столиков. </summary>
[Route("api/[controller]")]
[ApiController]
public class TableController : Controller
{
    private readonly ITableRepository _repository;
    private readonly ILogger _logger;

    /// <summary> Конструктор контроллера. </summary>
    /// <param name="repository"> Репозиторий. </param>
    /// <param name="logger"> Логгер. </param>
    public TableController(ITableRepository repository, ILogger<TableController> logger)
    {
        _repository = repository;
        _logger = logger;
        _logger.LogDebug(1, "Логгер встроен в TableController");
    }

    /// <summary> Создать столик. </summary>
    /// <param name="table"> Экземпляр столика. </param>
    /// <returns></returns>
    [HttpPost("CreateTable")]
    public IActionResult Create(TableModel table)
    {
        _logger.LogInformation(1, "Выполнение запроса на создание экземпляра TableModel в БД.");
        _repository.CreateAsync(table);
        return Ok();
    }

    /// <summary> Удалить столик по номеру. </summary>
    /// <param name="id"> Номер столика. </param>
    /// <returns></returns>
    [HttpPost("DeleteTable")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation(1, "Выполнение запроса на удаление экземпляра TableModel из БД.");
        _repository.DeleteAsync(id);
        return Ok();
    }

    /// <summary> Получить столик по номеру. </summary>
    /// <param name="id"> Номер столика. </param>
    /// <returns> Экземпляр столика. </returns>
    [HttpGet("GetTable")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation(1, "Выполнение запроса на получение экземпляра TableModel из БД.");
        var table = await _repository.GetByIdAsync(id);
        if(table is null) return NotFound();
        return Ok(table);
    }

    /// <summary> Зарезервировать столик по кол-ву мест. Асинхронно. </summary>
    /// <param name="seatsCount"> Минимальное кол-во мест. </param>
    /// <returns> Текстовое описание результата операции. </returns>
    [HttpPatch("BookingTableAsync")]
    public async Task<IActionResult> BookingTableAsync(int seatsCount)
    {
        _logger.LogInformation(1, "Выполнение запроса на асинхронное бронирование экземпляра TableModel в БД.");
        new ConsoleWriter().PhoneGreeting();
        int tableNumb = await _repository.BookingTableAsync(seatsCount);

        if (tableNumb == -1) return Ok("К сожалению сейчас все столики заняты.");
        return Ok($"Готово! Ваш столик номер {tableNumb}");
    }

    /// <summary> Зарезервировать столик по кол-ву мест. </summary>
    /// <param name="seatsCount"> Минимальное кол-во мест. </param>
    /// <returns> Текстовое описание результата операции. </returns>
    [HttpPatch("BookingTable")]
    public IActionResult BookingTable(int seatsCount)
    {
        _logger.LogInformation(1, "Выполнение запроса на бронирование экземпляра TableModel в БД.");
        new ConsoleWriter().MessageGreeting();
        int tableNumb = _repository.BookingTable(seatsCount);

        if (tableNumb == -1) return Ok("К сожалению сейчас все столики заняты.");
        return Ok($"УВЕДОМЛЕНИЕ: Готово! Ваш столик номер {tableNumb}");
    }

    /// <summary> Зарезервировать столик по номеру. Асинхронно. </summary>
    /// <param name="tableNumb"> Номер столика. </param>
    /// <returns> Текстовое описание результата операции. </returns>
    [HttpPatch("BookingTableByNumbAsync")]
    public async Task<IActionResult> BookingTableByNumbAsync(int tableNumb)
    {
        _logger.LogInformation(1, "Выполнение запроса на асинхронное бронирование экземпляра TableModel в БД.");
        new ConsoleWriter().PhoneGreeting();
        bool result = await _repository.BookingTableByNumbAsync(tableNumb);

        if (!result) return Ok("К сожалению сейчас все столики заняты.");
        return Ok($"Готово! Ваш столик номер {tableNumb}");
    }

    /// <summary> Зарезервировать столик по номеру. </summary>
    /// <param name="tableNumb"> Номер столика. </param>
    /// <returns> Текстовое описание результата операции. </returns>
    [HttpPatch("BookingTableByNumb")]
    public IActionResult BookingTableByNumb(int tableNumb)
    {
        _logger.LogInformation(1, "Выполнение запроса на бронирование экземпляра TableModel в БД.");
        new ConsoleWriter().MessageGreeting();
        bool result = _repository.BookingTableByNumb(tableNumb);

        if (!result) return Ok("К сожалению сейчас все столики заняты.");
        return Ok($"УВЕДОМЛЕНИЕ: Готово! Ваш столик номер {tableNumb}");
    }

    /// <summary> Обновить данные столика. </summary>
    /// <param name="table"> Экземпляр столика с новыми данными. </param>
    /// <returns></returns>
    [HttpPatch("Update")]
    public IActionResult Update(TableModel table)
    {
        _logger.LogInformation(1, "Выполнение запроса на обновление экземпляра TableModel в БД.");
        _repository.UpdateAsync(table);
        return Ok();
    }
}
