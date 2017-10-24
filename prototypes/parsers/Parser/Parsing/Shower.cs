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

        public void SaveSpecialities(List<Speciality> specialities)
        {
            throw new NotImplementedException();
        }
    }

   
}
