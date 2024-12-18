namespace WebApp.Models;

public class CurrentDateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentDateTime()
    {
        return DateTime.Now;
    }
}