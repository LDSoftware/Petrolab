namespace PetroLabWebAPI.Services.Helpers;

public interface IScheduleGeneratorService
{
    List<DateTime> GenerateSchedule(DateTime startDate, DateTime endDate);
    List<DateTime> GenerateScheduleHour(DateTime date, string startHour, string endHour, int interval);
    string GetDayOfWeek(string day);
}
