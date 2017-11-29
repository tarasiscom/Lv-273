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
        /// <returns>User personal information</returns>
        UserPersonalInfo PersonalInfo();

        /// <summary>
        /// This method retrieves user favorites specialtys
        /// </summary>
        /// <returns>Favorites specialtys list</returns>
        IEnumerable<Specialty> GetFavoriteSpecialty(int page);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int CountOfFavoriteSpecialtys();

        /// <summary>
        /// Add selected specialty to favorites
        /// </summary>
        /// <param name="UserId">User Id</param>
        /// <param name="SpecialtyId">Specialty Id </param>
        void AddSpecialtyToFavorite(string UserId,int SpecialtyId);
    }
}
