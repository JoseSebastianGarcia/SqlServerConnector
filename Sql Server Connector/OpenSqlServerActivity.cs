using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Data.SqlClient;

namespace Sql_Server_Connector
{
    
    public class OpenSqlServerActivity : CodeActivity
    {
        [Category("Input")]
        [Description("Connection string to be used")]
        public InArgument<string> ConnectionString { set; get; }
        
        [Category("Impersonation options")]
        [Description("User to impersonate. Ej. \"domain\\user\".")]
        public InArgument<string> User { set; get; }
        
        [Category("Impersonation options")]
        [Description("Password of the user")]
        public InArgument<string> Password { set; get; }
        
        [Category("Impersonation options")]
        [Description("When setting to TRUE, it will be necessary to write a [domain\\user] and [password] to impersonate")]
        public InArgument<bool> Impersonate { set; get; }

        [Category("Output")]
        [Description("The resulting connection object instance is obtained")]
        public OutArgument<SqlConnection> Connection { set; get; }

        [STAThread]
        protected override void Execute(CodeActivityContext context)
        {
            string connectionString = ConnectionString.Get(context);
            string user = User.Get(context);
            string password = Password.Get(context);
            bool impersonate = Impersonate.Get(context);

            SqlConnection connection = SqlServerCore.Open(connectionString, user, password, impersonate);

            Connection.Set(context,connection);

        }
    }
}
