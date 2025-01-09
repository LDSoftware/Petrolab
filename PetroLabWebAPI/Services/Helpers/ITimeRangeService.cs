namespace PetroLabWebAPI.Services.Helpers;

public interface ITimeRangeService<T>
    {
        T Start { get; }
        T End { get; }
        bool Includes(T value);
        bool Includes(ITimeRangeService<T> range);
    }
