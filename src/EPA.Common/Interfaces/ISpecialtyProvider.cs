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
        ///  This method retrieves more detailed information about specific test
        /// </summary>
        ///  <param name="listOfSubjects"> subjects for the ZNO </param>
        ///  <returns> more detatiled test information </returns>
        IEnumerable<Specialty> GetSpecialtyBySubjects(List<int> listOfSubjects);
        
        /// <summary>
        ///  This method retrives list of all Subjects for ZNO
        /// </summary>
        /// <returns>List of subjects</returns>
        IEnumerable<Subject> GetAllSubjects();

        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="idDirection"> id of the general direction </param>
        /// <returns> List of specialties </returns>
        IEnumerable<Specialty> GetSpecialtiesByDirection(int idDirection); //temp

        /// <summary>
        /// This method retrives list of general directions
        /// </summary>
        /// <returns> List of general directions </returns>
        IEnumerable<GeneralDirection> GetGeneralDirections();

        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="idDirection"> id of the general direction </param>
        /// <returns> Limited list of specialties </returns>
        IEnumerable<Specialty> GetSpecialtiesByDirectionWithPagination(int idDirection, int page);
    }
}
