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
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/User/FavoriteSpecialties")]
        [HttpPost]
        [Authorize]
        public IEnumerable<Specialty> GetFavoriteSpecialties([FromBody] int page)
        {
            var a = this.GetUserId(this.User);
            return this.userInformationProvider.GetFavoriteSpecialty(this.GetUserId(this.User),page, a);
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
              return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/User/GetSpecialtiesCount")]
        public Count GetSpecialtiesCount() => this.userInformationProvider.CountOfFavoriteSpecialtys(null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/User/GetUserPersonalInformation")]
        public UserPersonalInfo GetUserPersonalInfo()
        {
            var a = this.GetUserId(this.User);
            var abc = this.userInformationProvider.GetPersonalInfo(a);
            return abc;
        }


        [Route("api/user/AddToFav/{id:int}")]
        [HttpGet]
        public bool AddToFavorite(int id)
        {
            return userInformationProvider.AddSpecialtyToFavorite(this.GetUserId(this.User), id);
        }

        [Route("api/user/RemoveFromFav/{id:int}")]
        [HttpGet]
        public bool RemoveFromFavorite(int id)
        {
            return userInformationProvider.RemoveSpecialtyFromFavorite(this.GetUserId(this.User), id);
        }
    }
}