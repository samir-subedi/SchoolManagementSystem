using MySql.Data.MySqlClient;
namespace ManagementSystem
{
	public class SqlConnection
	{
		private static string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=DETAILS;";

        public static MySqlConnection GetConnection()
		{
			return new MySqlConnection(connectionString);
		}
    }
}
