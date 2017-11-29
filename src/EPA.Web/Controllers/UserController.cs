using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for User opearations
    /// </summary>
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
        public IEnumerable<Specialty> GetFavoriteSpecialties([FromBody] int page) => this.userInformationProvider.GetFavoriteSpecialty(page, null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/User/GetSpecialtiesCount")]
        public Count GetSpecialtiesCount() => this.userInformationProvider.CountOfFavoriteSpecialtys(null);
    }
}