using System.Globalization;

namespace Common;

public class Clock : IClock
{
    private readonly TimeSpan _initialShift;

    public Clock() : this(DateTime.Now)
    {
    }

    public Clock(DateTime currentDateTime)
    {
        _initialShift = DateTime.Now - currentDateTime;
    }

    public DateTime Now => DateTime.Now - _initialShift;
    public DateOnly Today => new(Now.Year, Now.Month, Now.Day);

    public DateOnly GetNearestDayOfWeek(DayOfWeek dayOfWeek)
    {
        int daysToAdd = ((int)dayOfWeek - (int)Today.DayOfWeek + 7) % 7;

        if (daysToAdd == 0)
        {
            daysToAdd += 7;
        }

        return Today.AddDays(daysToAdd);
    }

    public DateOnly GetTheLastDayInTheCurrentMonth()
    {
        var beginOfTheCurrentMonth = new DateOnly(Now.Year, Now.Month, 1);
        return beginOfTheCurrentMonth.AddMonths(1).AddDays(-1);
    }

    public DateOnly GetTheNextMonthBegin()
    {
        DateTime nextMonthDate = Now.AddMonths(1);
        return new DateOnly(nextMonthDate.Year, nextMonthDate.Month, 1);
    }

    public DateTime UtcNow => Now.ToUniversalTime();
}