namespace PetroLabWebAPI.Data.ExecutionModel;

public class CommonExecutionModel
{
    public long ResultId { get; set; }
    public int Code { get; set; }
    public string Message { get; set; } = null!;
    public bool Success
    {
        get
        {
            return Code.Equals(200) ? true : false;
        }
    }
}