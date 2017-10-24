using Parsing.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Parsing
{


    class ShowInConsole: ISaver
    {
        public void SaveDistrict(District district)
        {
            Console.WriteLine(String.Format("districtID{0}\n {1}",district.ID, district.Name));
        }

        public void SaveUniversity(University university)
        {
            Console.WriteLine(String.Format("District: {4}I\n D:{0} \n УНІВЕРСИТЕТ: {1} \n Адреса:{2} \n Сайт:{3}",university.ID, university.Name, university.Adress, university.Site, university.District));
            Console.WriteLine("СПЕЦІАЛЬНОСТІ: \n");
        }

        public void SaveDirections(IEnumerable<Direction> directions)
        {
            foreach (Direction direction in directions)
            {
                Console.WriteLine(String.Format("Галузь:{0} ID:{1} UninerID:{2}",direction.Name, direction.ID, direction.UniversityID));
            }
        }

        public void SaveSpecialities(IEnumerable<Speciality> specialities)
        {
            foreach (Speciality speciality in specialities)
            {
                string s = String.Format("Spes: {0}\n ID{1}\n Галузь{2}", speciality.Name, speciality.ID, speciality.FacultyID );
                Console.WriteLine(s);
                Console.WriteLine("_____________________________________________________");
 
            }
        }
    }

    class DatabaseSaver : ISaver
    {
        string connectionString;
        string formUniversity = "({0},{1},{2},{3},{4})";
        string formDirection = "({0},{1},{2})";
        string formSpeciality = "({0},{1},{2})";
        SqlCommand commandUniversity = new SqlCommand();
        SqlCommand commandDirections = new SqlCommand();
        SqlCommand commandSpeciality = new SqlCommand();

        public void SaveDirections(IEnumerable<Direction> directions)
        {
            StringBuilder queryDirections = new StringBuilder("INSERT INTO Directions VALUES ");

            foreach(Direction direction in directions)
            {
                queryDirections.Append(String.Format(formDirection, direction.ID, direction.UniversityID, direction.Name));
                queryDirections.Append(",");
            }

            queryDirections.Append(";");
            commandDirections.CommandText = queryDirections.ToString();
        }

        public void SaveSpecialities(IEnumerable<Speciality> specialities)
        {
            throw new NotImplementedException();
        }

        public void SaveUniversity(University university)
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                commandDirections.ExecuteScalar();

            }
        }
    }
}
