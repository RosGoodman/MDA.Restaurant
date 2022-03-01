using Common.DAL.Repositories;
using Quartz;

namespace MDA.Restaurant.Jobs;

/// <summary> Задача по снятию бронирования столиков. </summary>
public class RemovingTheReservationsJob : IJob
{
    private readonly ITableRepository _repository;

    public RemovingTheReservationsJob(ITableRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _repository.RemovingAllReservationsAsync();
    }
}
