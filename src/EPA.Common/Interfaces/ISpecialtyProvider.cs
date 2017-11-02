using System.Collections.Generic;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
<<<<<<< HEAD
    public interface ISpecialtyProvider
    {
        /// <summary>
        ///  This method retrieves more detailed information about specific test
        /// </summary>
        ///  <param name="id"> id of the test </param>
        ///  <returns> more detatiled test information </returns>
        IEnumerable<Specialty> GetSpecialtyBySubjects(List<Subject> listOfSubjects);
=======
    /// <summary>
    ///  This interface describes methods for getting Specialty and Direction related data
    /// </summary>
    public interface ISpecialtyProvider
    {
        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="idDirection"> id of the general direction </param>
        /// <returns> List of specialties </returns>
        IEnumerable<Specialty> GetSpecialtiesByDirection(int idDirection);

        /// <summary>
        /// This method retrives list of general directions
        /// </summary>
        /// <returns> List of general directions </returns>
        IEnumerable<GeneralDirection> GetGeneralDirections();

>>>>>>> origin/GeneralDirection_MSSQLandCommon
    }
}
