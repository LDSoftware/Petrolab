using System.Data;
using Dapper;
using PetroLabWebAPI.Data.DataConnection;

namespace PetroLabWebAPI.Data.Repository;

public class StoredProcRepository(DapperContext _context)
{
    private string _storedProc = string.Empty;
    private DynamicParameters _dynamicParameters = null!;

    public virtual StoredProcRepository Initialize(string storedProc, DynamicParameters dynamicParameters)
    {
        _storedProc = storedProc;
        _dynamicParameters = dynamicParameters;
        return this;
    }

    public virtual async Task<TResponse?> Execute<TResponse>()
    {
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection
                .QueryAsync<TResponse>(_storedProc,
                _dynamicParameters, commandType: CommandType.StoredProcedure);

            return result!.First();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default(TResponse);
        }
    }

    public virtual async Task<TResponse> ReturnSingle<TResponse>()
    where TResponse : class
    {
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection
                .QueryAsync<TResponse>(_storedProc,
                _dynamicParameters, commandType: CommandType.StoredProcedure);

            return result!.First();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;
        }
    }

    public virtual async Task<IList<TResponse>?> ReturnCollection<TResponse>()
    where TResponse : class
    {
        try
        {
            using var connection = _context.CreateConnection();
            var result = await connection
                .QueryAsync<TResponse>(_storedProc,
                _dynamicParameters, commandType: CommandType.StoredProcedure);

            return result!.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null!;
        }
    }    
}
