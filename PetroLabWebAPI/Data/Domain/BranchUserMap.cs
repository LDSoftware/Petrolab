namespace PetroLabWebAPI.Data.Domain;

public record BranchUserMap(long Id, string UserId, long BranchId);
public record BranchUserMapView(long Id, long BranchId, string BranchName, string UserId);