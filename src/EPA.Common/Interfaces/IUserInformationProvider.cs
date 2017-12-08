using EPA.Common.DTO.UserProvider;
using EPA.Common.DTO;
using System.Collections.Generic;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// This interface describes methods for getting user related data
    /// </summary>
    public interface IUserInformationProvider
    {
        /// <summary>
        /// This method retrieves user's personal information
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>User personal information</returns>
        UserPersonalInfo GetPersonalInfo(string userId);

        /// <summary>
        /// This method retrieves user's favorite specialties
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="page">page iterator</param>
        /// <returns>Collection of favorite specialties</returns>
        IEnumerable<Specialty> GetFavoriteSpecialty(string userId, int page);

        /// <summary>
        /// This method retrieves count of favorite specialties
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Count of favorite specialties</returns>
        Count CountOfFavoriteSpecialtys(string userId);

        /// <summary>
        /// This method returns a list of test for which we have saved results
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns> Collection of tests </returns>
        IEnumerable<Test> GetTestResults(string userId);

        /// <summary>
        /// This method returns test results for a specific test and user
        /// </summary>
        /// <param name="testId">Selected test</param>
        /// <param name="userId">User Id</param>
        /// <returns> Test Results for a selected tests</returns>
        IEnumerable<DirectionScores> GetTestResult(int testId, string userId);

        /// <summary>
        /// This method adds selected specialty to favorites
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="specialtyId">Specialty Id </param>
        /// <returns> Logical flag that represents operation status</returns>
        bool AddSpecialtyToFavorite(string userId, int specialtyId);

        /// <summary>
        /// This method removes specialty from favorites
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="specialtyId">Specialty Id</param>
        /// <returns> Logical flag that represents operation status</returns>
        bool RemoveSpecialtyFromFavorite(string userId, int specialtyId);
    }
}
