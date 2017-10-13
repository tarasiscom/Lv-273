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
        public void SaveDistrict(District deistrict)
        {
            Console.WriteLine(String.Format("{0}", deistrict.Name));
        }

        public void SaveUniversity(University university)
        {
            Console.WriteLine(String.Format("УНІВЕРСИТЕТ: {0}", university.Name));
        }

        public void SaveFaculties(IEnumerable<Faculty> faculties)
        {
            Console.WriteLine(String.Format("{0} область"));
        }

        public void SaveSpecialities(IEnumerable<Speciality> specialities)
        {
            foreach (Speciality speciality in specialities)
            {
                string s = String.Format("Галузь: {0}\n Спеціальність{1}\n Факультет{2}", speciality.Name, speciality.MyProperty, speciality.MyProperty2 );
                Console.WriteLine(s);
                Console.WriteLine("_____________________________________________________");
 
            }	
        }
    }
}
