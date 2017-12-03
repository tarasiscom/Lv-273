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
        /// This mehod retrives list of top universities
        /// </summary>
        /// <returns>List of subjects</returns>
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
            var data = this.universitiesProvider.GetLogoById(id);
            byte[] imgData = data.FirstOrDefault();
            return this.File(imgData, "image/jpeg");
        }
    }
}