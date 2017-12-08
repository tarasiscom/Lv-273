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
        /// This method retrieve user's favorite specialties
        /// </summary>
        /// <param name="page">Page iterator</param>
        /// <returns>List of Specialties</returns>
        [Route("api/User/FavoriteSpecialties/{page:int}")]
        [HttpGet]
        public IEnumerable<Specialty> GetFavoriteSpecialties(int page)
        {
            var userId = this.GetUserId(this.User);
            return this.userInformationProvider.GetFavoriteSpecialty(userId, page);
        }

        /// <summary>
        /// This method retrieves count of user's favorite specialties
        /// </summary>
        /// <returns>Count</returns>
        [Route("api/User/GetSpecialtiesCount")]
        public Count GetSpecialtiesCount() => this.userInformationProvider.CountOfFavoriteSpecialtys(this.GetUserId(this.User));

        /// <summary>
        /// This method retrieves user's personal information
        /// </summary>
        /// <returns> User personal information </returns>
        [Route("api/User/GetUserPersonalInformation")]
        public UserPersonalInfo GetUserPersonalInfo()
        {
            var userId = this.GetUserId(this.User);
            return this.userInformationProvider.GetPersonalInfo(userId);
        }

        /// <summary>
        /// This method returns a list of test for which we have saved results
        /// </summary>
        /// <returns> Collection of tests </returns>
        [Route("api/User/GetTestResults")]
        [HttpGet]
        public IEnumerable<Test> GetTestResults()
        {
            var userId = this.GetUserId(this.User);
            return this.userInformationProvider.GetTestResults(userId);
        }

        /// <summary>
        /// This method returns test results for a specific test and user
        /// </summary>
        /// <param name="id">Selected test</param>
        /// <returns> Test Results for a selected tests</returns>
        [Route("api/User/GetTestResult/{id:int}")]
        [HttpGet]
        public IEnumerable<DirectionScores> GetTestResult(int id)
        {
            var userId = this.GetUserId(this.User);
            return this.userInformationProvider.GetTestResult(id, userId);
        }

        /// <summary>
        /// This method adds selected specialty to favorites
        /// </summary>
        /// <param name="id">Specialty Id </param>
        /// <returns> Logical flag that represents operation status</returns>
        [Route("api/User/AddToFav/{id:int}")]
        [HttpGet]
        public bool AddToFavorite(int id)
        {
            return this.userInformationProvider.AddSpecialtyToFavorite(this.GetUserId(this.User), id);
        }

        /// <summary>
        /// This method removes specialty from favorites
        /// </summary>
        /// <param name="id">Specialty Id</param>
        /// <returns> Logical flag that represents operation status</returns>
        [Route("api/User/RemoveFromFav/{id:int}")]
        [HttpGet]
        public bool RemoveFromFavorite(int id)
        {
            return this.userInformationProvider.RemoveSpecialtyFromFavorite(this.GetUserId(this.User), id);
        }

        /// <summary>
        /// This method gives user Id.
        /// </summary>
        /// <param name="principal">Claim Principal</param>
        /// <returns>User ID</returns>
        private string GetUserId(ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}