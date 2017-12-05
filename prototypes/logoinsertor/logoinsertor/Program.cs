using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace logoinsertor
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string input = "F:\\Kpi.jpg";
            byte[] readText = File.ReadAllBytes(input);
            
            //used stored procedure from database for SqlCommand
            SqlCommand sqlCommand = new SqlCommand("Logo_Universities_INS", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            var logoParam = new SqlParameter("logo", SqlDbType.VarBinary);
            logoParam.Value = readText;
            sqlCommand.Parameters.Add(logoParam);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
