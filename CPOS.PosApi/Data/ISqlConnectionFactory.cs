using Microsoft.Data.SqlClient;

namespace CPOS.PosApi.Data;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}
