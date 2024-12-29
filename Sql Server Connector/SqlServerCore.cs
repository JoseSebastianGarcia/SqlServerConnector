using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using SimpleImpersonation;
using System.Security.Principal;
namespace Sql_Server_Connector
{
    public static class SqlServerCore
    {
        [STAThread]
        public static SqlConnection Open(string connectionString, string user, string password, bool impersonate) 
        {
            SqlConnection connection;

            if (!impersonate)
            {
                connection = new SqlConnection(connectionString); 
                connection.Open();
            }
            else 
            {
                UserCredentials userCredentials = new UserCredentials(user, password);
                
                var handler = userCredentials.LogonUser(LogonType.Interactive);

				connection = WindowsIdentity.RunImpersonated<SqlConnection>(handler, () => {
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    return sqlConnection;
                });
            }

            
            return connection;
        }

        [STAThread]
        public static DataSet ExecuteQuery(SqlConnection connection, CommandType commandType, string query, Dictionary<string, object> parameters = null) 
        {
            DataSet result = new DataSet();

            if (connection.State == ConnectionState.Open)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = query;
                    command.CommandType = commandType;

                    if (!(parameters == null || parameters.Count == 0))
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                        }
                    }

                    command.Connection = connection;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }
            else 
            {
                throw new Exception("The connection is closed");
            }

            return result;
        }

        [STAThread]
        public static void Close(SqlConnection connection) 
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
            else
                throw new Exception("The connection is closed");
        }
            
    }
}
