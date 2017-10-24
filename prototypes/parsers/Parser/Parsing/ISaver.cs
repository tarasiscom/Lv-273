using Parsing.DataClasses;
using System.Collections.Generic;

namespace Parsing
{
    public interface ISaver
    {
        //void SaveDistrict(District deistrict);
        void SaveUniversities(IEnumerable<University> universities);
        void SaveDirections(IEnumerable<Direction> faculties);
        void SaveSpecialities(List<Speciality> specialities);
        void SaveAll();
    }
}
