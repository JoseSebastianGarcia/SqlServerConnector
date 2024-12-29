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
    
    public class ExecuteQuerySqlServerActivity : CodeActivity
    {
        [Category("Input")]
        [Description("Connection to be used")]
        public InArgument<SqlConnection> Connection { set; get; }
        
        [Category("Command")]
        [Description("Type")]
        public InArgument<CommandType> CommandType { set; get; }
        
        [Category("Command")]
        [Description("Text")]
        public InArgument<string> CommandText { set; get; }

        [Category("Command")]
        [Description("Parameters: IList<string, object>")]
        public InArgument<Dictionary<string,object>> CommandParameters { set; get; }



        [Category("Output")]
        [Description("If the query returns data, it will be received in a DataSet")]
        public OutArgument<DataSet> Result { set; get; }
        protected override void Execute(CodeActivityContext context)
        {
            SqlConnection sqlConnection = context.GetValue(Connection);
            CommandType commandType = context.GetValue(CommandType);
            string commandText = context.GetValue(CommandText);
            Dictionary<string, object> commandParameters = context.GetValue(CommandParameters);

            DataSet result = SqlServerCore.ExecuteQuery(
                    sqlConnection,
                    commandType,
                    commandText,
                    commandParameters
                );
            
            context.SetValue(Result, result);

        }
    }
}
