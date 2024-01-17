namespace Common;

public interface IClock
{
    DateTime Now { get; }
    DateOnly Today { get; }
    DateTime UtcNow { get; }
    DateOnly GetNearestDayOfWeek(DayOfWeek dayOfWeek);
    DateOnly GetTheLastDayInTheCurrentMonth();
    DateOnly GetTheNextMonthBegin();
}