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
        [Route("api/choosessubject")]
        public IEnumerable<Subject> GetSubject() { return new Subject[] { new Subject { Name = "Матемаьтка" }, new Subject { Name = "Українська" }, new Subject { Name = "Українська" } }; } ///=> this.specialtyProvider.GetAllSubjects();

        /// <summary>
        /// his method retrives list of specialties according to subjects
        /// </summary>
        /// <param name="listOfSubjects">List of subject</param>
        /// <returns>List of specialties </returns>
        [Route("api/choosespeciliti/busubject")]
        [HttpPost]
        public IEnumerable<Specialty> GetSpecialtyBySubjects([FromBody]List<int> listOfSubjects) => this.specialtyProvider.GetSpecialtyBySubjects(listOfSubjects);

        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="idDirection"> id of the general direction </param>
        /// <returns> List of specialties </returns>
        [Route("api/choosespeciality/bydirection/{idDirection}")]
        [HttpGet("{idDirection}")]
        public IEnumerable<Specialty> GetSpecialtiesByDirection(int idDirection) =>
                                      this.specialtyProvider.GetSpecialtiesByDirection(idDirection);

        /// <summary>
        /// This method retrives list of general directions
        /// </summary>
        /// <returns> List of general directions </returns>
        [Route("api/choosespeciality/getdirection")]
        public IEnumerable<GeneralDirection> GetGeneralDirection() => this.specialtyProvider.GetGeneralDirections();
    }
}