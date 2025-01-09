namespace PetroLabWebAPI.Services.Helpers;

    public class TimeRangeService : ITimeRangeService<DateTime>
	{
		public TimeRangeService(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public bool Includes(DateTime value)
        {
            return (Start <= value) && (value <= End);
        }

        public bool Includes(ITimeRangeService<DateTime> range)
        {
            return (Includes(range.Start) || Includes(range.End));
        }
    }
