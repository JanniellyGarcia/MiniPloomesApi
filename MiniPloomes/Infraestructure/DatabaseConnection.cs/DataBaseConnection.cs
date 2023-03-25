using System.Data.SqlClient;

namespace MiniPloomes.Infraestructure.DatabaseConnection.cs
{
    public class DataBaseConnection
    {
        public SqlConnection? SqlConnection { get; set; }
        public SqlCommand? SqlCommand { get; set; }
        public SqlDataReader? SqlDataReader { get; set; }

        public void GetConnection()
        {
            SqlConnection = new SqlConnection("Server=./;DataBase= Mini_Ploomes");
            SqlConnection.Open();
        }
        public void CloseConnection()
        {
            SqlConnection.Close();
        }
    }
}
