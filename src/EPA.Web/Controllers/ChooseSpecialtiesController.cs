﻿using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Specialty data draws
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
        /// <param name="subjectInfo">List of subject</param>
        /// <returns>List of specialties </returns>
        [Route("api/ChooseSpecialties/bySubject")]
        [HttpPost]
        public IEnumerable<Specialty> GetSpecialtyBySubjects([FromBody] SubjectsInfo subjectInfo)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.specialtyProvider.GetSpecialtyBySubjects(this.User.FindFirstValue(ClaimTypes.NameIdentifier), subjectInfo.ListSubjects, subjectInfo.District, subjectInfo.Page);
            }
            else
            {
                return this.specialtyProvider.GetSpecialtyBySubjects(string.Empty, subjectInfo.ListSubjects, subjectInfo.District, subjectInfo.Page);
            }
        }

        /// <summary>
        /// This method retrives list of specialties according to general direction
        /// </summary>
        /// <param name="idDirection">Id of the general direction</param>
        /// <param name="idDistrict">Id of the district</param>
        /// <param name="page">Page iterator</param>
        /// <returns>List of specialties</returns>
        [Route("api/ChooseSpecialties/byDirectionAndDistrict/{idDirection}/{idDistrict}/{page}")]
        [HttpGet]
        public IEnumerable<Specialty> GetSpecialtiesByDirection(int idDirection, int idDistrict, int page)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.specialtyProvider.GetSpecialtiesByDirection(this.User.FindFirstValue(ClaimTypes.NameIdentifier), idDirection, idDistrict, page);
            }
            else
            {
                return this.specialtyProvider.GetSpecialtiesByDirection(string.Empty, idDirection, idDistrict, page);
            }
        }

        /// <summary>
        /// This method retrieves count of pages for getting specialties by general direction
        /// </summary>
        /// <param name="idDirection">Id of the general direction</param>
        /// <param name="idDistrict">Id of the district</param>
        /// <returns>Count</returns>
        [Route("api/ChooseSpecialties/count/{idDirection}/{idDistrict}")]
        [HttpGet]
        public Count GetCountByDirection(int idDirection, int idDistrict)
        {
            return this.specialtyProvider.GetCountByDirection(idDirection, idDistrict);
        }

        /// <summary>
        /// This method retrieves count of pages for getting specialties by subjects
        /// </summary>
        /// <param name="subjectInfo">Subjects information</param>
        /// <returns>Count</returns>
        [Route("api/ChooseSpecialties/count/bySubjects")]
        [HttpPost]
        public Count GetCountBySubjects([FromBody] SubjectsInfo subjectInfo)
        {
            return this.specialtyProvider.GetCountBySubjects(subjectInfo.ListSubjects, subjectInfo.District);
        }
    }
}