using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces;
using EPA.Common.DTO;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Specialty and Direction draws
    /// </summary>
    public class ChooseSpecialityController : Controller
    {
        private readonly ISpecialtyProvider specialtyProvider;

        public ChooseSpecialityController(ISpecialtyProvider specialtyProvider)
        {
            this.specialtyProvider = specialtyProvider;
        }

        /// <summary>
        /// This mehod retrives list of subjects
        /// </summary>
        /// <returns>List of subjects</returns>
        [Route("api/ChooseUniversity/ChoseSpecBySub")]
        public IEnumerable<Subject> GetAllSubjects() => this.specialtyProvider.GetAllSubjects();

        /// <summary>
        /// This method retrives list of districts
        /// </summary>
        /// <returns>List of districts</returns>
        [Route("api/ChooseUniversity/ChoseSpecDistrictList")]
        public IEnumerable<District> GetAllDistrict() => this.specialtyProvider.GetAllDistricts();

        /// <summary>
        /// This method retrives list of specialties according to subjects
        /// </summary>
        /// <param name="subjectsAndDistrict">List of subject</param>
        /// <returns>List of specialties </returns>
        [Route("api/ChooseUniversity/ChoseSpecBySublist")]
        [HttpPost]
        public SpecialtiesAndCount GetSpecialtyBySubjects([FromBody] ListSubjectsAndDistrict subjectsAndDistrict) //=> this.specialtyProvider.GetSpecialtyBySubjects(subjectsAndDistrict);
        {
            var x=this.specialtyProvider.GetSpecialtyBySubjects(subjectsAndDistrict);
            return x;
        }
        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="directionAndDistrict"> Contains id of the general direction and district id </param>
        /// <returns> List of specialties </returns>
        [Route("api/choosespeciality/bydirection")]
        [HttpPost]
        public IEnumerable<Specialty> GetSpecialtiesByDirection([FromBody]DirectionAndDistrict directionAndDistrict) => this.specialtyProvider.GetSpecialtiesByDirection(directionAndDistrict);

        /// <summary>
        /// This method retrives list of general directions
        /// </summary>
        /// <returns> List of general directions </returns>
        [Route("api/choosespeciality/getdirection")]
        public IEnumerable<GeneralDirection> GetGeneralDirection() => this.specialtyProvider.GetGeneralDirections();
    }
}