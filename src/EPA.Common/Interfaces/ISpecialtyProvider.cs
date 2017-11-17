using System.Collections.Generic;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    /// <summary>
    ///  This interface describes methods for getting Specialty and Direction related data
    /// </summary>
    public interface ISpecialtyProvider
    {
        /// <summary>
        ///  This method retrieves information about specialties 
        /// </summary>
        ///  <param name="listOfSubjects"> subjects for the ZNO </param>
        ///  <returns> List of Specialties </returns>
        Specialties GetSpecialtyBySubjects(ListSubjectsAndDistrict listOfSubjects);
        
        /// <summary>
        ///  This method retrives list of all Subjects
        /// </summary>
        /// <returns>List of subjects</returns>
        IEnumerable<Subject> GetAllSubjects();

        /// <summary>
        ///  This method retrives list of all Districts 
        /// </summary>
        /// <returns>List of Districts</returns>
        IEnumerable<District> GetAllDistricts();

        /// <summary>
        /// This method retrives list of specialties according to general direction and district
        /// </summary>
        /// <param name="directionAndDistrictInfo"> Contains id of the general direction, district id, number of page and bumber of elements per page</param>
        /// <returns> Limited list of specialties and count of all specialties </returns>
        Specialties GetSpecialtiesByDirectionAndDistrict(DirectionAndDistrictInfo directionAndDistrictInfo);

        /// <summary>
        /// This method retrives list of specialties according to general direction 
        /// </summary>
        /// <param name="directionInfo"> Contains id of the general direction, number of page and bumber of elements per page</param>
        /// <returns> Limited list of specialties and count of all specialties </returns>
        Specialties GetSpecialtiesByDirection(DirectionInfo directionInfo);

        /// <summary>
        /// This method retrives list of general directions
        /// </summary>
        /// <returns> List of general directions </returns>
        IEnumerable<GeneralDirection> GetGeneralDirections();
    }
}
