using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Specialty and Direction draws
    /// </summary>
    public class UniversitiesController : Controller
    {
        private readonly IUniversitiesProvider universitiesProvider;

        public UniversitiesController(IUniversitiesProvider universitiesProvider)
        {
            this.universitiesProvider = universitiesProvider;
        }

        /// <summary>
        /// This mehod retrives list of subjects
        /// </summary>
        /// <returns>List of subjects</returns>
        [Route("api/Universities/getTopUniversities")]
        [HttpGet]
        public IEnumerable<University> GetTopUniversities()
        {
            return this.universitiesProvider.GetTopUniversities();
        }


        [Route("api/Universities/{id:int}/logo")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var data = this.universitiesProvider.GetLogoById(id);
            byte[] imgData = data.FirstOrDefault();
            return this.File(imgData, "image/jpeg");
        }
    }
}