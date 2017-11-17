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
        /// <param name="subjects">List of subject</param>
        /// <returns>List of specialties </returns>
        [Route("api/ChooseSpecialties/bySubject")]
        [HttpPost]
        public IEnumerable<Specialty> GetSpecialtyBySubjects([FromBody] SubjectsInfo subjects)
        {
            return this.specialtyProvider.GetSpecialtyBySubjects(subjects);
        }

        /// <summary>
        /// This method retrives list of specialties according to general direction and district
        /// </summary>
        /// <param name="direction"> Contains id of the general direction,
        /// district id, number of page and bumber of elements per page</param>
        /// <returns> Limited list of specialties and count of all specialities</returns>
        [Route("api/ChooseSpecialties/byDirectionAndDistrict")]
        [HttpPost]
        public IEnumerable<Specialty> GetSpecialtiesByDirectionAndDistrict([FromBody] DirectionInfo direction)
        {
            return this.specialtyProvider.GetSpecialtiesByDirectionAndDistrict(direction);
        }

        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="directionInfo"> Contains id of the general direction,
        /// number of page and number of elements per page</param>
        /// <returns> Limited list of specialties and count of all specialities </returns>
        [Route("api/ChooseSpecialties/byDirection")]
        [HttpPost]
        public IEnumerable<Specialty> GetSpecialtiesByDirection([FromBody] DirectionInfo directionInfo)
        {
            return this.specialtyProvider.GetSpecialtiesByDirection(directionInfo);
        }
    }
}