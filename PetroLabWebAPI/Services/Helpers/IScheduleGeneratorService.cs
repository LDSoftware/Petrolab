namespace PetroLabWebAPI.Services.Helpers;

public interface IScheduleGeneratorService
{
    List<DateTime> GenerateSchedule(int month);
    List<DateTime> GenerateScheduleHour(DateTime date, string startHour, string endHour);
    string GetDayOfWeek(string day);
}
