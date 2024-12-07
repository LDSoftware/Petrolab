using Dapper;
using PetroLabWebAPI.Data.ExecutionModel;

namespace PetroLabWebAPI.Data.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    IRepository<TEntity> Initialize(string storedProc, DynamicParameters dynamicParameters);
    Task<CommonExecutionModel> InsertOrUpdate();
    Task<CommonExecutionModel> Delete();
    Task<IList<TEntity>> Table();
    Task<TEntity> GetById();
}
