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
        //потрібен для витягання повного ліста всіх спеціальностей. Тобто фактично не потрібен. 
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

        /// <summary>
        /// This method retrives list of specialties according to general direction ordered by rating.
        /// </summary>
        /// <returns> Limited list of specialities </returns>
        [Route("api/choosespeciality/bydirection/{idDirection}")]
        [HttpPost]
        public IEnumerable<Specialty> GetSpecialtiesByDirectionWithPagination(int idDirection, [FromBody]int page) =>
                      this.specialtyProvider.GetSpecialtiesByDirectionWithPagination(idDirection, page);
    }
}