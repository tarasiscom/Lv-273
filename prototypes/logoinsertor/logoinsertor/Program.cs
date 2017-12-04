using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Data;

namespace logoinsertor
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=ssu-sql12\\tc;Database=EpaDb;User Id=Lv-273.Net;Password=Lv-273.Ne;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            byte[] readText = File.ReadAllBytes("F:\\Kpi.jpg");
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
