using System.Data;
using Microsoft.Data.SqlClient;

namespace PetroLabWebAPI.Data.DataConnection;

public class DapperContext(IConfiguration _configuration)
{
    public IDbConnection CreateConnection()
        => new SqlConnection(_configuration.GetConnectionString("DataConnection")!);
}