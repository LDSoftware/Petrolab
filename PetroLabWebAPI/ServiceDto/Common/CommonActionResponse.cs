namespace PetroLabWebAPI.ServiceDto.Common;

public record CommonActionResponse(
    int Code = DefaultExecutionStatus.DefaultCode,
    string Message = DefaultExecutionStatus.MessageDefault);

public record CommonActionCreateUser(
    string UserId = DefaultExecutionStatus.ReturnEmpty,
    int Code = DefaultExecutionStatus.DefaultCode,
    string Message = DefaultExecutionStatus.MessageDefault) : CommonActionResponse(Code, Message);
