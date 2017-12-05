using EPA.Common.DTO;
using System.Collections.Generic;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// Interface that contains methods for getting University and Logo of University related data
    /// </summary>
    public interface IUniversitiesProvider
    {
        /// <summary>
        /// This method retrieves collection of top universities from database
        /// </summary>
        /// <returns>Collection of top universities</returns>
        IEnumerable<University> GetTopUniversities();

        /// <summary>
        /// This method retrieves Logo of University by column LogoId from the table Universities
        /// </summary>
        /// <param name="id">Id from table Logo_Universities</param>
        /// <returns>logo of University</returns>
        IEnumerable<byte[]> GetLogoById(int id);

        /// <summary>
        /// Retrieves all universities in current district
        /// </summary>
        /// <param name="district">District Id</param>
        /// <returns>List of universities</returns>
        IEnumerable<University> GetAllUniversitiesInDistrict(int district);
    }
}
