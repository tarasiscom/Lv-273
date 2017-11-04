using System;
using System.Collections.Generic;
using System.Text;
using Parsing.DataClasses;
using System.Data.SqlClient;

namespace Parsing
{
    public class DatabaseSaver : ISaver
    {
        private static readonly int size = 500;

        private string connectionString = @"Server=ssu-sql12\tc;Database=EpaDb;User Id=Lv-273.Net;Password=Lv-273.Ne";
        private string formDistrict = "({0},N'{1}')";
        private string formUniversity = "({0},N'{1}',{2},N'{3}',N'{4}')";
        private string formDirection = "({0},N'{1}',{2})";
        private string formSpeciality = "({0},{1},N'{2}',{3},{4},{5})";
        private string formSubject = "({0},N'{1}')";
        private string formSpecSub = "({0},{1},{2})";

        private SqlCommand commandUniversity = new SqlCommand();
        private SqlCommand commandDirections = new SqlCommand();
        private SqlCommand commandSpeciality = new SqlCommand();
        private SqlCommand commandSubject = new SqlCommand();
        private SqlCommand commandSpecSub = new SqlCommand();
        private SqlCommand commandDistrict = new SqlCommand();

        private List<string> partsSpecialities = new List<string>();
        private List<string> partsSpecSubs = new List<string>();

        private StringBuilder querySpecialities = new StringBuilder();

        public void SaveDirections(List<Direction> directions)
        {
            StringBuilder queryDirections = new StringBuilder("INSERT INTO Directions VALUES ");

            foreach (Direction direction in directions)
            {
                queryDirections.Append(String.Format(formDirection, direction.ID, direction.Name.Replace("'", "`"), 1));
                queryDirections.Append(",");
            }

            queryDirections.Remove(queryDirections.Length - 1, 1).Append(';');
            commandDirections.CommandText = queryDirections.ToString();
        }

        public void SaveSpecialities(List<Speciality> specialities)
        {
            foreach (var page in specialities.Partition(size))
            {
                var query = GenerateQuerySpecialities(page);
                partsSpecialities.Add(query);
            }
        }

        public void SaveSpecSubs(List<SpecSub> specsubs)
        {
            foreach (var page in specsubs.Partition(size))
            {
                var query = GenerateQuerySpecSubs(page);
                partsSpecSubs.Add(query);
            }
        }

        public void SaveUniversities(List<University> universities)
        {
            StringBuilder queryUniversity = new StringBuilder("INSERT INTO Universities VALUES ");

            foreach (University university in universities)
            {
                queryUniversity.Append(String.Format(formUniversity, university.ID, university.Adress.Replace("'", "`"), university.DistrictID, university.Name.Replace("'", "`"), university.Site.Replace("'", "`")));
                queryUniversity.Append(",");
            }

            queryUniversity.Remove(queryUniversity.Length - 1, 1).Append(';').Replace("&#34;", "\"");
            commandUniversity.CommandText = queryUniversity.ToString();
        }

        private string GenerateQuerySpecialities(IEnumerable<Speciality> specialities)
        {
            StringBuilder querySpeciality = new StringBuilder("INSERT INTO Specialties VALUES ");

            foreach (Speciality speciality in specialities)
            {
                querySpeciality.Append(String.Format(formSpeciality, speciality.ID, speciality.DirectionID, speciality.Name.Replace("'", "`"), speciality.UniversityID, speciality.ApplicationsAmount, speciality.EnrolledAmount));
                querySpeciality.Append(",");
            }

            return querySpeciality.Remove(querySpeciality.Length - 1, 1).Append(';').ToString();
        }

        private string EscapeString(string str) => str?.Replace("'", "`");

        public void SaveAll(List<University> universities, List<Direction> directions, List<Speciality> specialities, List<Subject> subjects, List<SpecSub> specsubs, List<District> districts)
        {
            SaveUniversities(universities);
            SaveDirections(directions);
            SaveSpecialities(specialities);
            SaveSpecSubs(specsubs);
            SaveSubjects(subjects);
            SaveDistricts(districts);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                commandDistrict.Connection = connection;
                commandUniversity.Connection = connection;
                commandSpeciality.Connection = connection;
                commandSubject.Connection = connection;
                commandSpecSub.Connection = connection;
                commandDirections.Connection = connection;

                connection.Open();
                commandDistrict.ExecuteNonQuery();
                commandUniversity.ExecuteNonQuery();
                commandDirections.ExecuteNonQuery();
                foreach (string part in partsSpecialities)
                {
                    commandSpeciality.CommandText = part;
                    commandSpeciality.ExecuteNonQuery();
                }
                commandSubject.ExecuteNonQuery();
                foreach (string part in partsSpecSubs)
                {
                    commandSpecSub.CommandText = part;
                    commandSpecSub.ExecuteNonQuery();
                }
            }
        }

        public void SaveSubjects(List<Subject> subjects)
        {
            StringBuilder querySubjects = new StringBuilder("INSERT INTO Subjects VALUES ");

            foreach (Subject subject in subjects)
            {
                querySubjects.Append(String.Format(formSubject, subject.ID, subject.Name.Replace("'", "`")));
                querySubjects.Append(",");
            }

            querySubjects.Remove(querySubjects.Length - 1, 1).Append(';');
            commandSubject.CommandText = querySubjects.ToString();
        }

        public string GenerateQuerySpecSubs(IEnumerable<SpecSub> specsubs)
        {
            StringBuilder querySpecSub = new StringBuilder("INSERT INTO Specialty_Subjects VALUES ");

            foreach (SpecSub specsub in specsubs)
            {
                querySpecSub.Append(String.Format(formSpecSub, specsub.ID, specsub.SubjectId, specsub.SpecialityId ));
                querySpecSub.Append(",");
            }

            return querySpecSub.Remove(querySpecSub.Length - 1, 1).Append(';').ToString();
        }

        public void SaveDistricts(List<District> districts)
        {
            StringBuilder queryDistricts = new StringBuilder("INSERT INTO Districts VALUES ");

            foreach (District district in districts)
            {
                queryDistricts.Append(String.Format(formDistrict, district.ID, district.Name.Replace("'", "`")));
                queryDistricts.Append(",");
            }

            queryDistricts.Remove(queryDistricts.Length - 1, 1).Append(';');
            commandDistrict.CommandText = queryDistricts.ToString();
        }
    }
}









