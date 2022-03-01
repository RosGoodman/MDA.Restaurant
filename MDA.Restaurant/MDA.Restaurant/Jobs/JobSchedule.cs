namespace MDA.Restaurant.Jobs;

/// <summary> DTO-класс для хранения расписания запуска. </summary>
public class JobSchedule
{
    public Type JobType { get; }
    public string CronExpression { get; }

    public JobSchedule(Type jobType, string cronExpression)
    {
        JobType = jobType;
        CronExpression = cronExpression;
    }
}
