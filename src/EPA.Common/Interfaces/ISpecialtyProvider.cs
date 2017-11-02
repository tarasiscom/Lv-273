using System.Collections.Generic;
using EPA.Common.DTO;

namespace EPA.Common.Interfaces
{
    public interface ISpecialtyProvider
    {
        /// <summary>
        ///  This method retrieves more detailed information about specific test
        /// </summary>
        ///  <param name="id"> id of the test </param>
        ///  <returns> more detatiled test information </returns>
        IEnumerable<Specialty> GetSpecialtyBySubjects(List<Subject> listOfSubjects);
    }
}
