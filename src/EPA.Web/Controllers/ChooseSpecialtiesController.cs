using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces;
using EPA.Common.DTO;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Specialty and Direction draws
    /// </summary>
    public class ChooseSpecialtiesController : Controller
    {
        private readonly ISpecialtyProvider specialtyProvider;

        public ChooseSpecialtiesController(ISpecialtyProvider specialtyProvider)
        {
            this.specialtyProvider = specialtyProvider;
        }

        /// <summary>
        /// This mehod retrives list of subjects
        /// </summary>
        /// <returns>List of subjects</returns>
        [Route("api/ChooseSpecialties/ChoseSpecBySub")]
        public IEnumerable<Subject> GetAllSubjects() => this.specialtyProvider.GetAllSubjects();

        /// <summary>
        /// This method retrives list of districts
        /// </summary>
        /// <returns>List of districts</returns>
        [Route("api/ChooseSpecialties/ChoseSpecDistrictList")]
        public IEnumerable<District> GetAllDistrict() => this.specialtyProvider.GetAllDistricts();

        /// <summary>
        /// This method retrives list of specialties according to subjects
        /// </summary>
        /// <param name="subjectsAndDistrict">List of subject</param>
        /// <returns>List of specialties </returns>
        [Route("api/ChooseSpecialties/ChoseSpecBySublist")]
        [HttpPost]
        public Specialties GetSpecialtyBySubjects([FromBody] ListSubjectsAndDistrict subjectsAndDistrict)
        {
            return this.specialtyProvider.GetSpecialtyBySubjects(subjectsAndDistrict);
        }

        /// <summary>
        /// This method retrives list of specialties according to general direction and district
        /// </summary>
        /// <param name="directionAndDistrictInfo"> Contains id of the general direction,
        /// district id, number of page and bumber of elements per page</param>
        /// <returns> Limited list of specialties and count of all specialities</returns>
        [Route("api/ChooseSpecialties/bydirection")]
        [HttpPost]
        public Specialties GetSpecialtiesByDirectionAndDistrict([FromBody]DirectionAndDistrictInfo directionAndDistrictInfo)
        {
            return this.specialtyProvider.GetSpecialtiesByDirectionAndDistrict(directionAndDistrictInfo);
        }

        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="directionInfo"> Contains id of the general direction,
        /// number of page and number of elements per page</param>
        /// <returns> Limited list of specialties and count of all specialities </returns>
        [Route("api/ChooseSpecialties/bydirectiononly")]
        [HttpPost]
        public Specialties GetSpecialtiesByDirection([FromBody]DirectionInfo directionInfo)
        {
            return this.specialtyProvider.GetSpecialtiesByDirection(directionInfo);
        }

        /// <summary>
        /// This method retrives list of general directions
        /// </summary>
        /// <returns> List of general directions </returns>
        [Route("api/ChooseSpecialties/getdirection")]
        public IEnumerable<GeneralDirection> GetGeneralDirection() => this.specialtyProvider.GetGeneralDirections();
    }
}