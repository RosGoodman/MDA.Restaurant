using Common.DAL.Models;
using Common.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDA.Restaurant.Controllers;

/// <summary> Контроллер ресторана. </summary>
[Route("api/[controller]")]
[ApiController]
public class RestaurantController : Controller
{
    private readonly IRestaurantRepository _repository;
    private readonly ILogger _logger;

    /// <summary> Конструктор класса. </summary>
    /// <param name="repository"> Репозиторий. </param>
    /// <param name="logger"> Логгер. </param>
    public RestaurantController(IRestaurantRepository repository, ILogger<RestaurantController> logger)
    {
        _repository = repository;
        _logger = logger;
        _logger.LogDebug(1, "Логгер встроен в RestaurantController");
    }

    /// <summary> Создать ресторан. </summary>
    /// <param name="restaurant"> Экземпляр ресторана. </param>
    /// <returns></returns>
    [HttpPost("CreateRestaurant")]
    public IActionResult Create(RestaurantModel restaurant)
    {
        _logger.LogInformation(1, "Выполнение запроса на создание экземпляра RestaurantModel в БД.");
        _repository.CreateAsync(restaurant);
        return Ok();
    }

    /// <summary> Удалить ресторан по ID. </summary>
    /// <param name="id"> ID ресторана. </param>
    /// <returns></returns>
    [HttpPost("DeleteRestaurant")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation(1, "Выполнение запроса на удаление экземпляра RestaurantModel из БД.");
        _repository.DeleteAsync(id);
        return Ok();
    }

    /// <summary> Получить ресторан по ID. </summary>
    /// <param name="id"> ID ресторана. </param>
    /// <returns> Экземпляр ресторана. </returns>
    [HttpGet("GetRestaurant")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation(1, "Выполнение запроса на получение экземпляра RestaurantModel из БД.");
        var restaurant = await _repository.GetByIdAsync(id);
        if (restaurant is null) return NotFound();
        return Ok(restaurant);
    }

    /// <summary> Обновить данные ресторана. </summary>
    /// <param name="table"> Экземпляр с новыми данными. </param>
    /// <returns></returns>
    [HttpPatch("UpdateRestaurant")]
    public IActionResult UpdateAsync(RestaurantModel table)
    {
        _logger.LogInformation(1, "Выполнение запроса на асинхронное обновление экземпляра RestaurantModel в БД.");
        _repository.UpdateAsync(table);
        return Ok();
    }
}
