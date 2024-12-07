using System.Data;
using Dapper;
using PetroLabWebAPI.Data.DataConnection;
using PetroLabWebAPI.Data.ExecutionModel;

namespace PetroLabWebAPI.Data.Repository;

public class GenericRepository<TEntity>(DapperContext _context)
     : IRepository<TEntity> where TEntity : class
{
    private string _storedProc = string.Empty;
    private DynamicParameters _dynamicParameters = null!;

    public virtual IRepository<TEntity> Initialize(string storedProc, DynamicParameters dynamicParameters)
    {
        _storedProc = storedProc;
        _dynamicParameters = dynamicParameters;
        return this;
    }

    public virtual async Task<CommonExecutionModel> Delete()
    {
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection
                .QueryAsync<CommonExecutionModel>(_storedProc,
                _dynamicParameters, commandType: CommandType.StoredProcedure);
            if (result != null && result.Any())
            {
                if (result.First().Code.Equals(500))
                {
                    throw new Exception(result.First().Message);
                }
            }

            return result!.First();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new()
            {
                Code = 500,
                Message = ex.Message
            };
        }
    }

    public virtual async Task<TEntity> GetById()
    {
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection
                .QueryAsync<TEntity>(_storedProc,
                _dynamicParameters, commandType: CommandType.StoredProcedure);

            return result!.First();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    public virtual async Task<CommonExecutionModel> InsertOrUpdate()
    {
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection
                .QueryAsync<CommonExecutionModel>(_storedProc,
                _dynamicParameters, commandType: CommandType.StoredProcedure);
            if (result != null && result.Any())
            {
                if (result.First().Code.Equals(500))
                {
                    throw new Exception(result.First().Message);
                }
            }

            return result!.First();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new()
            {
                Code = 500,
                Message = ex.Message
            };
        }
    }

    public virtual async Task<IList<TEntity>> Table()
    {
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection
                .QueryAsync<TEntity>(_storedProc,
                _dynamicParameters, commandType: CommandType.StoredProcedure);

            return result!.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    public virtual async Task<CommonExecutionModel> Update()
    {
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection
                .QueryAsync<CommonExecutionModel>(_storedProc,
                _dynamicParameters, commandType: CommandType.StoredProcedure);
            if (result != null && result.Any())
            {
                if (result.First().Code.Equals(500))
                {
                    throw new Exception(result.First().Message);
                }
            }

            return result!.First();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new()
            {
                Code = 500,
                Message = ex.Message
            };
        }
    }
}

