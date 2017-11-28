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

        [Route("api/User/FavoriteSpecialties")]
        public IEnumerable<Specialty> GetFavoriteSpecialties() => this.userInformationProvider.GetFavoriteSpecialty();
    }
}