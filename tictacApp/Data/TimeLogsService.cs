namespace tictacApp.Data;

public class TimeLogsService
{
    public Task<TimeLog[]> GetTimeLogsAsync(DateTime currentDateTime)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new TimeLog
        {
            Id = index,
            Date = currentDateTime.AddDays(-index),
            Description = "coucou",
        }).ToArray());
    }
}