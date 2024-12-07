namespace PetroLabWebAPI.ServiceDto.Common;

public record CreateActionResponse(long Id, int Code = DefaultExecutionStatus.DefaultCode, 
    string Message = DefaultExecutionStatus.MessageDefault) : CommonActionResponse(Code,Message);
