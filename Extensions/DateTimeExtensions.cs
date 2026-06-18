namespace TraineeManagement.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToUtcSecondPrecision(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
    }
}
 