using Common.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDA.Restaurant.Controllers;

[Route("api/[contorller]")]
[ApiController]
public class TableController : Controller
{
    private readonly ITableRepository _repository;

    public TableController(ITableRepository repository)
    {
        _repository = repository;
    }


}
