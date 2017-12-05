using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using EPA.Common.DTO.UserProvider;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for User opearations
    /// </summary>
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserInformationProvider userInformationProvider;

        public UserController(IUserInformationProvider userInformationProvider)
        {
            this.userInformationProvider = userInformationProvider;
        }

        /// <summary>
        /// This method retrieve Users Favorite Specialties for page
        /// </summary>
        /// <param name="page"> Page </param>
        /// <returns>List of Specialties</returns>
        [Route("api/User/FavoriteSpecialties/{page:int}")]
        [HttpGet]
        public IEnumerable<Specialty> GetFavoriteSpecialties(int page)
        {
            var userId = this.GetUserId(this.User);
            return this.userInformationProvider.GetFavoriteSpecialty(userId, page);
        }

        /// <summary>
        /// This Method retrives authorized User Id
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>User Id</returns>
        public string GetUserId(ClaimsPrincipal principal)
        {
              return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        /// <summary>
        /// This method retrieves count of users favorite specialties
        /// </summary>
        /// <returns>Count</returns>
        [Authorize]
        [Route("api/User/GetSpecialtiesCount")]
        public Count GetSpecialtiesCount() => this.userInformationProvider.CountOfFavoriteSpecialtys(this.GetUserId(this.User));

        /// <summary>
        /// This method retrieves Users Personal Information
        /// </summary>
        /// <returns> UserPersonalInfo </returns>
        [Authorize]
        [Route("api/User/GetUserPersonalInformation")]
        public UserPersonalInfo GetUserPersonalInfo()
        {
            var userId = this.GetUserId(this.User);
            return this.userInformationProvider.GetPersonalInfo(userId);
        }

        [Route("api/user/AddToFav/{id:int}")]
        [HttpGet]
        public bool AddToFavorite(int id)
        {
            return this.userInformationProvider.AddSpecialtyToFavorite(this.GetUserId(this.User), id);
        }

        [Route("api/user/RemoveFromFav/{id:int}")]
        [HttpGet]
        public bool RemoveFromFavorite(int id)
        {
            return this.userInformationProvider.RemoveSpecialtyFromFavorite(this.GetUserId(this.User), id);
        }
    }
}