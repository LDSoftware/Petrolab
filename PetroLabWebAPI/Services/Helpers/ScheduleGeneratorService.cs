namespace PetroLabWebAPI.Services.Helpers;

public class ScheduleGeneratorService : IScheduleGeneratorService
{
    public List<DateTime> GenerateSchedule(int month)
    {
        var schedule = new List<DateTime>();
        var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, month);
        for (int i = 1; i <= daysInMonth; i++)
        {
            schedule.Add(new DateTime(DateTime.Now.Year, month, i));
        }
        return schedule;
    }

    public List<DateTime> GenerateScheduleHour(DateTime date, string startHour, string endHour)
    {
        DateTime startDate = DateTime.Parse($"{date.ToShortDateString()} {startHour}");
        DateTime endDate = DateTime.Parse($"{date.ToShortDateString()} {endHour}");
        List<DateTime> schedule = new();

        while (startDate < endDate)
        {
            schedule.Add(startDate);
            startDate = startDate.AddHours(1);
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
