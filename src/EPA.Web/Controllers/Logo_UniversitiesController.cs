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
    public class Logo_UniversitiesController : Controller
    {
        private readonly ILogo_UniversitiesProvider logo_universitiesProvider;

        public Logo_UniversitiesController(ILogo_UniversitiesProvider logo_universitiesProvider)
        {
            this.logo_universitiesProvider = logo_universitiesProvider;
        }

        [Route("api/Universities/listId")]
        [HttpGet]
        public List<int> GetId()
        {
            return this.logo_universitiesProvider.GetId();
        }
       
    }
}