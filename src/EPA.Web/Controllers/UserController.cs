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
        public IEnumerable<Specialty> GetFavoriteSpecialties() => this.userInformationProvider.GetFavoriteSpecialty(0);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/User/GetSpecialtiesCount")]
        public int GetSpecialtiesCount() => this.userInformationProvider.CountOfFavoriteSpecialtys();
    }
}