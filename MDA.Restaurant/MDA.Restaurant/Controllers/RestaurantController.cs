using Common.DAL.Models;
using Common.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDA.Restaurant.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController : Controller
{
    private readonly IRestaurantRepository _repository;
    private readonly ILogger _logger;

    public RestaurantController(IRestaurantRepository repository, ILogger<RestaurantController> logger)
    {
        _repository = repository;
        _logger = logger;
        _logger.LogDebug(1, "Логгер встроен в RestaurantController");
    }

    [HttpPost("CreateRestaurant")]
    public IActionResult Create(RestaurantModel restaurant)
    {
        _logger.LogInformation(1, "Выполнение запроса на создание экземпляра RestaurantModel в БД.");
        _repository.CreateAsync(restaurant);
        return Ok();
    }

    [HttpPost("DeleteRestaurant")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation(1, "Выполнение запроса на удаление экземпляра RestaurantModel из БД.");
        _repository.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("GetRestaurant")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation(1, "Выполнение запроса на получение экземпляра RestaurantModel из БД.");
        var restaurant = await _repository.GetByIdAsync(id);
        if (restaurant is null) return NotFound();
        return Ok(restaurant);
    }

    [HttpPatch("UpdateRestaurant")]
    public IActionResult UpdateAsync(RestaurantModel table)
    {
        _logger.LogInformation(1, "Выполнение запроса на асинхронное обновление экземпляра RestaurantModel в БД.");
        _repository.UpdateAsync(table);
        return Ok();
    }
}
