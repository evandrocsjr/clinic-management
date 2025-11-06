namespace Wpm.Clinic.Domain.ValuesObjects;

public record DateTimeRange
{
    public DateTime StartedAt { get; init; }
    public DateTime? EndedAt { get; init; }

    public DateTimeRange(DateTime startedAt, DateTime? endedAt)
    {
        ValidateRange(startedAt, endedAt);
        StartedAt = startedAt;
        EndedAt = endedAt;
    }

    public DateTimeRange(DateTime startedAt)
    {
        StartedAt = startedAt;
    }

    private void ValidateRange(DateTime startedAt, DateTime? endedAt)
    {
        if (endedAt < startedAt)
            throw new InvalidOperationException("Invalid date and time range.");
    }

    public string Duration
    {
        get
        {
            if (EndedAt == null)
            {
                return "Ongoing";
            }   
            var duration = EndedAt.Value - StartedAt;
            return $"Duration: {duration.Days} days, {duration.Hours} hours, {duration.Minutes} minutes";
        }
    }

    public static implicit operator DateTimeRange(DateTime dateTime)
    {
        return new DateTimeRange(dateTime);
    }
    
};