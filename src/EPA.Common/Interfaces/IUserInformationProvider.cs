using EPA.Common.DTO.UserProvider;
using EPA.Common.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPA.Common.Interfaces
{
    public interface IUserInformationProvider
    {
        /// <summary>
        /// This method retrieves user personal information
        /// </summary>
        /// <param name="Id">User Id</param>
        /// <returns>User personal information</returns>
        UserPersonalInfo GetPersonalInfo(string Id);

        /// <summary>
        /// This method retrieves user favorites specialtys
        /// </summary>
        /// <returns>Favorites specialtys list</returns>
        IEnumerable<Specialty> GetFavoriteSpecialty(string userId, int page, string UserID);

        /// <summary>
        /// This method retrieves count of Favorite Specialtys
        /// </summary>
        /// <param name="UserID">User id</param>
        /// <returns>Count of favorite specialtys</returns>
        Count CountOfFavoriteSpecialtys(string UserID);

        /// <summary>
        /// Add selected specialty to favorites
        /// </summary>
        /// <param name="UserId">User Id</param>
        /// <param name="SpecialtyId">Specialty Id </param>
        bool AddSpecialtyToFavorite(string UserId,int SpecialtyId);

        /// <summary>
        /// This method remove specialty from favorite
        /// </summary>
        /// <param name="UserId">User Id</param>
        /// <param name="SpecialtyId">Specialty Id</param>
        bool RemoveSpecialtyFromFavorite(string UserId, int SpecialtyId);
    }
}
