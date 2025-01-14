namespace PetroLabWebAPI.Services.Helpers;

public class ScheduleGeneratorService : IScheduleGeneratorService
{
    public List<DateTime> GenerateSchedule(DateTime startDate, DateTime endDate)
    {
        var schedule = new List<DateTime>();
        while (startDate <= endDate)
        {
            schedule.Add(startDate);
            startDate = startDate.AddDays(1);
        }
        return schedule;
    }

    public List<DateTime> GenerateScheduleHour(DateTime date, string startHour, string endHour, int interval)
    {
        DateTime startDate = DateTime.Parse($"{date.ToShortDateString()} {startHour}");
        DateTime endDate = DateTime.Parse($"{date.ToShortDateString()} {endHour}");
        List<DateTime> schedule = new();

        while (startDate <= endDate)
        {
            schedule.Add(startDate);
            startDate = startDate.AddMinutes(interval);
        }

        return schedule;
    }

    public string GetDayOfWeek(string day)
    {
        return day switch
        {
            "Monday" => "Lunes",
            "Tuesday" => "Martes",
            "Wednesday" => "Miércoles",
            "Thursday" => "Jueves",
            "Friday" => "Viernes",
            "Saturday" => "Sábado",
            "Sunday" => "Domingo",
            _ => "Invalid day",
        };
    }
}
