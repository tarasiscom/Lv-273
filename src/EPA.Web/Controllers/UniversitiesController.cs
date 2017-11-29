using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Specialty and Direction draws
    /// </summary>
    public class UniversitiesController : Controller
    {
        private readonly IUniversitiesProdiver universitiesProvider;

        public UniversitiesController(IUniversitiesProdiver universitiesProvider)
        {
            this.universitiesProvider = universitiesProvider;
        }

        /// <summary>
        /// This mehod retrives list of subjects
        /// </summary>
        /// <returns>List of subjects</returns>
        [Route("api/Universities/getTopUniversities")]
        [HttpGet]
        public IEnumerable<UniversityInfo> GetTopUniversities()
        {
            return this.universitiesProvider.GetTopUniversities();
        }

        [Route("api/Universities/getImgSrc")]
        [HttpPost]
        public List<string> GetImgSrc([FromBody] IEnumerable<UniversityInfo> listUniversities)
        {
            List<string> listImgSrc = new List<string>();
            foreach (var university in listUniversities)
            {
                listImgSrc.Add("data:image/jpg;base64," + Convert.ToBase64String(university.Logo));
            }
            // <img className="img-univer" src={this.state.imgSrc[id]} width="100%" height="100%" />
            return listImgSrc;
        }
    }
}