using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parsing.DataClasses;
using System.Data.SqlClient;

namespace Parsing
{
    public class DatabaseSaver : ISaver
    {
        private static readonly int size = 500;

        string connectionString = "data source = .\\SQLEXPRESS; initial catalog = EPA; integrated security = true";
        string formUniversity = "({0},'{1}','{2}','{3}','{4}')";
        string formDirection = "({0},'{1}')";

        SqlCommand commandUniversity = new SqlCommand();
        SqlCommand commandDirections = new SqlCommand();
        SqlCommand commandSpeciality = new SqlCommand();

        List<string> parts = new List<string>();
        

        StringBuilder querySpecialities = new StringBuilder();

        public void SaveDirections(IEnumerable<Direction> directions)
        {
            StringBuilder queryDirections = new StringBuilder("INSERT INTO Directions VALUES ");

            foreach (Direction direction in directions)
            {
                queryDirections.Append(String.Format(formDirection, direction.ID, direction.Name.Replace("'", "`")));
                queryDirections.Append(",");
            }

            queryDirections.Remove(queryDirections.Length - 1, 1).Append(';');
            commandDirections.CommandText = queryDirections.ToString();
        }

        public void SaveSpecialities(List<Speciality> specialities)
        {
            foreach (var page in specialities.Partition(size))
            {
                var query = GenerateQuery(page);
                parts.Add(query);
            }
        }

        //private void SavePartSpecialities(string query)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        commandSpeciality.CommandText = query;
        //        commandSpeciality.Connection = connection;

        //        connection.Open();
        //        commandSpeciality.ExecuteNonQuery();
                
        //    }
        //}



        private string GenerateQuery(IEnumerable<Speciality> page)
        {
            return $"INSERT INTO Specialities VALUES {string.Join(",", page.Select(item => CreateRecord(item)))};";
        }

        private string CreateRecord(Speciality item)
        {
            var orderedProperties = new string[] {
                    item.ID.ToString(),
                    item.DirectionID.ToString(),
                    item.UniversityID.ToString(),"'",
                    EscapeString(item.Name),"'"
            };

            return $"({string.Join(", ", orderedProperties)})";
        }

        private string EscapeString(string str) => str?.Replace("'", "`");

        public void SaveAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                commandDirections.Connection = connection;
                commandUniversity.Connection = connection;
                commandSpeciality.Connection = connection;

                connection.Open();
                commandUniversity.ExecuteScalar();
                commandDirections.ExecuteScalar();
                foreach (string part in parts)
                {
                    commandSpeciality.CommandText = part;
                    commandSpeciality.ExecuteNonQuery();
                }
            }
        }

        public void SaveUniversities(IEnumerable<University> universities)
        {
            StringBuilder queryUniversity = new StringBuilder("INSERT INTO Universities VALUES ");

            foreach (University university in universities)
            {
                queryUniversity.Append(String.Format(formUniversity, university.ID, university.District.Replace("'", "`"), university.Name.Replace("'", "`"), university.Adress.Replace("'", "`"), university.Site.Replace("'", "`")));
                queryUniversity.Append(",");
            }

            queryUniversity.Remove(queryUniversity.Length - 1, 1).Append(';').Replace("&#34", "\"");
            commandUniversity.CommandText = queryUniversity.ToString();
        }
    }
}
