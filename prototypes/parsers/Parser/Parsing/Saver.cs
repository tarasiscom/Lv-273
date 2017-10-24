using Parsing.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Parsing
{
    public class ShowInConsole: ISaver
    {
        public void SaveAll()
        {
            throw new NotImplementedException();
        }

        public void SaveDistrict(District district)
        {
            Console.WriteLine(String.Format("districtID{0}\n {1}",district.ID, district.Name));
        }


        public void SaveUniversity(University university)
        {
            Console.WriteLine(String.Format("District: {4}I\n D:{0} \n УНІВЕРСИТЕТ: {1} \n Адреса:{2} \n Сайт:{3}",university.ID, university.Name, university.Adress, university.Site, university.District));
            Console.WriteLine("СПЕЦІАЛЬНОСТІ: \n");

            using (StreamWriter writer = new StreamWriter(File.OpenWrite("1.txt")))
            {
                writer.Write("INSERT INTO Universities VALUES" + String.Format("({0},'{1}','{2}','{3}','{4}')", university.ID, university.District, university.Name, university.Adress, university.Site));
            }
        }

        public void SaveDirections(IEnumerable<Direction> directions)
        {
            foreach (Direction direction in directions)
            {
                Console.WriteLine(String.Format("Галузь:{0} ID:{1} ",direction.Name, direction.ID));
            }
        }

        public void SaveSpecialities(IEnumerable<Speciality> specialities)
        {
            foreach (Speciality speciality in specialities)
            {
                string s = String.Format("Spes: {0}\n ID{1}\n Галузь{2}", speciality.Name, speciality.ID, speciality.DirectionID );
                Console.WriteLine(s);
                Console.WriteLine("_____________________________________________________");
 
            }
        }

        public void SaveUniversities(IEnumerable<University> universities)
        {
            throw new NotImplementedException();
        }
    }

    public class DatabaseSaver : ISaver
    {
        string connectionString = "data source = .\\SQLEXPRESS; initial catalog = EPA; integrated security = true";
        string formUniversity = "({0},'{1}','{2}','{3}','{4}')";
        string formDirection = "({0},'{1}')";
        string formSpeciality = "({0},{1},{2},'{3}')";
        SqlCommand commandUniversity = new SqlCommand();
        SqlCommand commandDirections = new SqlCommand();
        SqlCommand commandSpeciality = new SqlCommand();

        public void SaveDirections(IEnumerable<Direction> directions)
        {
            StringBuilder queryDirections = new StringBuilder("INSERT INTO Directions VALUES ");

            foreach(Direction direction in directions)
            {
                queryDirections.Append(String.Format(formDirection, direction.ID, direction.Name.Replace("'", "`")));
                queryDirections.Append(",");
            }

            queryDirections.Remove(queryDirections.Length - 1, 1).Append(';');
            commandDirections.CommandText = queryDirections.ToString();
        }

        public void SaveSpecialities(IEnumerable<Speciality> specialities)
        {
            StringBuilder querySpecialities = new StringBuilder("INSERT INTO Specialities VALUES ");

            foreach (Speciality speciality in specialities)
            {
                querySpecialities.Append(String.Format(formSpeciality, speciality.ID, speciality.DirectionID, speciality.UniversityID, speciality.Name.Replace("'", "`")));
                querySpecialities.Append(",");
            }

            querySpecialities.Remove(querySpecialities.Length - 1, 1).Append(';');
            commandSpeciality.CommandText = querySpecialities.ToString();
        }

            public void SaveAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                commandDirections.Connection = connection;
                commandSpeciality.Connection = connection;
                commandUniversity.Connection = connection;
                connection.Open();
                commandUniversity.ExecuteScalar();
                commandDirections.ExecuteScalar();
                commandSpeciality.ExecuteScalar();
                
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
