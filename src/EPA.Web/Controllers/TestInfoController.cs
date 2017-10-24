﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces;
using EPA.Common.DTO;
using EPA.Common.dto;

namespace EPA.Web.Controllers
{
    public class TestInfoController : Controller
    {
        private IProfTestInfoProvider profTestInfoProvider;
        private IProfTestResultProvider profTestResultProvider;

        public TestInfoController(IProfTestInfoProvider profTestInfoProvider , IProfTestResultProvider profTestResultProvider)
        {
            this.profTestInfoProvider = profTestInfoProvider;
            this.profTestResultProvider = profTestResultProvider;
        }

        /// <summary>
        ///  This method retrieves the list of available ProfTests
        /// </summary>
        // GET: api/profTest/list
        [Route("api/profTest/list")]
        public IEnumerable<CommonTestInfo> GetTests() => profTestInfoProvider.GetTests();


        /// <summary>
        ///  This method retrieves the testInfo for current ProfTest
        ///  <param>id of selected ProfTest</param>
        /// </summary>
        // GET: api/profTest/{id}/info
        [Route("api/profTest/{id}/info")]
        [HttpGet("{id}")]
        public CommonTestDetailedInfo GetTestInfo(int id) => profTestInfoProvider.GetTestInfo(id);

        /// <summary>
        ///  This method retrieves the result for current ProfTest
        ///  <param>id of selected ProfTest</param>
        /// </summary>
        // GET: api/profTest/{id}/result
    
        [Route("/api/profTest/{id}/result")]
        [HttpPost("{id}")]
        public ProfTestResult GetUserResult([FromBody]int points, [FromBody]int id) => profTestResultProvider.GetUserResult(points,id);
        
    }
}
