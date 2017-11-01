using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces;
using EPA.Common.DTO;

namespace EPA.Web.Controllers
{
    public class ChooseSpecialityController : Controller
    {
        private readonly ISpecialtyProvider specialtyProvider;

        public ChooseSpecialityController(ISpecialtyProvider specialtyProvider)
        {
            this.specialtyProvider = specialtyProvider;
        }

        [Route("api/choosespeciality/bydirection/{id}")]
        public IEnumerable<Specialty> GetSpecialtiesByDirection(int idDirection) => 
                                      this.specialtyProvider.GetSpecialtiesByDirection(idDirection);
    }
}