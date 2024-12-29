using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace Sql_Server_Connector
{
    
    public class CloseSqlServerActivity : CodeActivity
    {
        [Category("Input")]
        [Description("Connection to be used")]
        public InArgument<SqlConnection> Connection { set; get; }
        
        protected override void Execute(CodeActivityContext context)
        {
            SqlConnection sqlConnection = context.GetValue(Connection);
            SqlServerCore.Close(sqlConnection);
        }
    }
}
