using Parsing.DataClasses;
using System.Collections.Generic;

namespace Parsing
{
    interface  ISaver
    {
        //void SaveDistrict(District deistrict);
        void SaveUniversity(University university);
        void SaveDirections(IEnumerable<Direction> faculties);
        void SaveSpecialities(IEnumerable<Speciality> specialities);
    }
}
