using Sql_Server_Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var cx = SqlServerCore.Open("[your connection string]", "[networkid]", "[password]", true);
            SqlServerCore.ExecuteQuery(cx, System.Data.CommandType.StoredProcedure, "[dbo].[InsertLoggeduser]");
            SqlServerCore.Close(cx);

            Console.ReadKey();
        }
    }
}
