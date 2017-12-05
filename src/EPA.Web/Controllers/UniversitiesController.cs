using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for University draws
    /// </summary>
    public class UniversitiesController : Controller
    {
        private readonly IUniversitiesProvider universitiesProvider;
        private readonly ISpecialtyProvider specialtyProvider;

        public UniversitiesController(IUniversitiesProvider universitiesProvider, ISpecialtyProvider specialtyProvider)
        {
            this.universitiesProvider = universitiesProvider;
            this.specialtyProvider = specialtyProvider;
        }

        /// <summary>
        /// This mehod retrives list of top universities
        /// </summary>
        /// <returns>List of top universities</returns>
        [Route("api/Universities/getTopUniversities")]
        [HttpGet]
        public IEnumerable<University> GetTopUniversities()
        {
            return this.universitiesProvider.GetTopUniversities();
        }

        /// <summary>
        /// This method returns image(logo) of University by id
        /// </summary>
        /// <param name="id">Id of Logo from the table Logo_Universities</param>
        /// <returns>Logo of University</returns>
        [Route("api/Universities/{id:int}/logo")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            byte[] imgData = this.universitiesProvider.GetLogoById(id);
            return this.File(imgData, "image/jpeg");
        }

        /// <summary>
        /// Retrieves all universities in current district
        /// </summary>
        /// <param name="district">District Id</param>
        /// <returns>List of universities</returns>
        [Route("api/universities/{district}")]
        public IEnumerable<University> GetAllUniversitiesInDistrict(int district)
        {
            return this.universitiesProvider.GetAllUniversitiesInDistrict(district);
        }

        /// <summary>
        /// Returns specialties of current university and direction
        /// </summary>
        /// <param name="universityId">University Id</param>
        /// <param name="directionId">Direction Id</param>
        /// <returns>Collection of universities</returns>
        [Route("api/GetSpecialties/{universityId}/{directionId}")]
        public IEnumerable<Specialty> GetSpecialtiesInUniversity(int universityId, int directionId)
        {
            return this.specialtyProvider.GetSpecialtiesInUniversity(universityId, directionId);
        }
    }
}