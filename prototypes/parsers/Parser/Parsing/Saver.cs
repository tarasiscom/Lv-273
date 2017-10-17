using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parsing.DataClasses;

namespace Parsing
{
    interface ISaver
    {
        void SaveDistrict(District deistrict);
        void SaveUniversity(University university);
        void SaveFaculties(IEnumerable<Faculty> faculties);
        void SaveSpecialities(IEnumerable<Speciality> specialities);
    }

    class ShowInConsole: ISaver
    {
        public void SaveDistrict(District district)
        {
            Console.WriteLine(String.Format("districtID{0}\n {1}",district.ID, district.Name));
        }

        public void SaveUniversity(University university)
        {
            Console.WriteLine(String.Format("ID:{0} \n УНІВЕРСИТЕТ: {1} \n Адреса:{2} \n Сайт:{3}",university.ID, university.Name, university.Adress, university.Site));
            Console.WriteLine("СПЕЦІАЛЬНОСТІ: \n");
        }

        public void SaveFaculties(IEnumerable<Faculty> faculties)
        {
            Console.WriteLine(String.Format("{0} область"));
        }

        public void SaveSpecialities(IEnumerable<Speciality> specialities)
        {
            foreach (Speciality speciality in specialities)
            {
                string s = String.Format("Галузь: {0}\n Спеціальність{1}\n Факультет{2}", speciality.Name, speciality.DirectionID, speciality.FacultyID );
                Console.WriteLine(s);
                Console.WriteLine("_____________________________________________________");
 
            }	
        }
    }
}
