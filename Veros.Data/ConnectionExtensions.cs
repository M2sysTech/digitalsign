namespace Veros.Data
{
    using System.Data;

    public static class ConnectionExtensions
    {
        public static int ExecuteNonQuery(this IDbConnection connection, string sql)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            return command.ExecuteNonQuery();
        }

        public static IDataReader ExecuteReader(this IDbConnection connection, string sql)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            return command.ExecuteReader();
        }
    }
}
