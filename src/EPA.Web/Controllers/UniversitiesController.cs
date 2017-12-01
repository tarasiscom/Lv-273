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

        [Route("api/Universities/listId")]
        [HttpGet]
        public List<int> GetId()
        {
            return this.universitiesProvider.GetId();
        }


        [Route("api/Universities/{id:int}/logo")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var data = this.universitiesProvider.GetLogoById(id);
            byte[] imgData = data.FirstOrDefault();
            return File(imgData, "image/jpeg");

   
            //MemoryStream ms = new MemoryStream(imgData);
            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new StreamContent(ms);
            //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            //return response;
        }



        //[Route("api/Universities/getImgSrc")]
        //[HttpPost]
        //public List<string> GetImgSrc([FromBody] IEnumerable<University> listUniversities)
        //{
        //    List<string> listImgSrc = new List<string>();
        //    foreach (var university in listUniversities)
        //    {
        //        listImgSrc.Add("data:image/jpg;base64," + Convert.ToBase64String(university.Logo));
        //    }
        //    // <img className="img-univer" src={this.state.imgSrc[id]} width="100%" height="100%" />
        //    return listImgSrc;
        //}
    }
}