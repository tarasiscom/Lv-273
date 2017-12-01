using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Specialty and Direction draws
    /// </summary>
    [Authorize]
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
        [Route("api/ChooseSpecialties/subjectsList")]
        public IEnumerable<Subject> GetAllSubjects() => this.specialtyProvider.GetAllSubjects();

        /// <summary>
        /// This method retrives list of general directions
        /// </summary>
        /// <returns> List of general directions </returns>
        [Route("api/ChooseSpecialties/directionsList")]
        public IEnumerable<GeneralDirection> GetGeneralDirection() => this.specialtyProvider.GetGeneralDirections();

        /// <summary>
        /// This method retrives list of districts
        /// </summary>
        /// <returns>List of districts</returns>
        [Route("api/ChooseSpecialties/districtsList")]
        public IEnumerable<District> GetAllDistrict() => this.specialtyProvider.GetAllDistricts();

        /// <summary>
        /// This method retrives list of specialties according to subjects
        /// </summary>
        /// <param name="subjectInfo">List of subject</param>
        /// <returns>List of specialties </returns>
        [Route("api/ChooseSpecialties/bySubject")]
        [HttpPost]
        public IEnumerable<Specialty> GetSpecialtyBySubjects([FromBody] SubjectsInfo subjectInfo)
        {
            return this.specialtyProvider.GetSpecialtyBySubjects(subjectInfo.ListSubjects, subjectInfo.District, subjectInfo.Page);
        }

        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="idDirection">Direction id</param>
        /// id of the general direction
        /// <param name="idDistrict">District id</param>
        /// number of page and number of elements per page
        /// <param name="page">Page number</param>
        /// Limited list of specialties and count of all specialities
        /// <returns>List if specialties</returns>
        [Route("api/ChooseSpecialties/byDirectionAndDistrict/{idDirection}/{idDistrict}/{page}")]
        [HttpGet]
        public IEnumerable<Specialty> GetSpecialtiesByDirectionAndDistrict(int idDirection, int idDistrict, int page)
        {
            return this.specialtyProvider.GetSpecialtiesByDirectionAndDistrict(idDirection, idDistrict, page);
        }

        [Route("api/ChooseSpecialties/count/{idDirection}/{idDistrict}")]
        [HttpGet]
        public Count GetCountByDirection(int idDirection, int idDistrict)
        {
            return this.specialtyProvider.GetCountByDirection(idDirection, idDistrict);
        }

        [Route("api/ChooseSpecialties/count/bySubjects")]
        [HttpPost]
        public Count GetCountBySubjects([FromBody] SubjectsInfo subjectInfo)
        {
            return this.specialtyProvider.GetCountBySubjects(subjectInfo.ListSubjects, subjectInfo.District);
        }
    }
}