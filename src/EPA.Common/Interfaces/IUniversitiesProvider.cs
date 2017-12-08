using EPA.Common.DTO;
using System.Collections.Generic;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// This interface describes methods for getting university related data
    /// </summary>
    public interface IUniversitiesProvider
    {
        /// <summary>
        /// This method retrieves collection of top universities from database
        /// </summary>
        /// <returns>Collection of top universities</returns>
        IEnumerable<University> GetTopUniversities();

        /// <summary>
        /// This method retrieves Logo of University
        /// </summary>
        /// <param name="id">Selected university</param>
        /// <returns>Logo of university</returns>
        byte[] GetLogoById(int id);

        /// <summary>
        /// This method retrieves all universities in selected district
        /// </summary>
        /// <param name="districtId">Selected district</param>
        /// <returns>Collection of universities</returns>
        IEnumerable<University> GetAllUniversitiesInDistrict(int districtId);
    }
}
