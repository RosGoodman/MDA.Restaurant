using Common.DAL.Models;
using Common.DAL.Repositories;
using MDA.Restaurant.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDA.Restaurant.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TableController : Controller
{
    private readonly ITableRepository _repository;
    private readonly ILogger _logger;

    public TableController(ITableRepository repository, ILogger<TableController> logger)
    {
        _repository = repository;
        _logger = logger;
        _logger.LogDebug(1, "Логгер встроен в TableController");
    }

    [HttpPost("CreateTable")]
    public IActionResult Create(TableModel table)
    {
        _logger.LogInformation(1, "Выполнение запроса на создание экземпляра TableModel в БД.");
        _repository.CreateAsync(table);
        return Ok();
    }

    [HttpPost("DeleteTable")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation(1, "Выполнение запроса на удаление экземпляра TableModel из БД.");
        _repository.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("GetTable")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation(1, "Выполнение запроса на получение экземпляра TableModel из БД.");
        var table = await _repository.GetByIdAsync(id);
        if(table is null) return NotFound();
        return Ok(table);
    }

    [HttpPatch("BookingTableAsync")]
    public async Task<IActionResult> BookingTableAsync(int seatsCount)
    {
        _logger.LogInformation(1, "Выполнение запроса на асинхронное бронирование экземпляра TableModel в БД.");
        new ConsoleWriter().PhoneGreeting();
        int tableNumb = await _repository.BookingTableAsync(seatsCount);

        if (tableNumb == -1) return Ok("К сожалению сейчас все столики заняты.");
        return Ok($"Готово! Ваш столик номер {tableNumb}");
    }

    [HttpPatch("BookingTable")]
    public IActionResult BookingTable(int seatsCount)
    {
        _logger.LogInformation(1, "Выполнение запроса на бронирование экземпляра TableModel в БД.");
        new ConsoleWriter().MessageGreeting();
        int tableNumb = _repository.BookingTable(seatsCount);

        if (tableNumb == -1) return Ok("К сожалению сейчас все столики заняты.");
        return Ok($"УВЕДОМЛЕНИЕ: Готово! Ваш столик номер {tableNumb}");
    }

    [HttpPatch("Update")]
    public IActionResult Update(TableModel table)
    {
        _logger.LogInformation(1, "Выполнение запроса на обновление экземпляра TableModel в БД.");
        _repository.UpdateAsync(table);
        return Ok();
    }
}
