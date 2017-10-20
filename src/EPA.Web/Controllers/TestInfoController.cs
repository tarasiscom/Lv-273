using System;
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
        /// <summary>
        /// interface IProfTestInfoProvider with methods: 
        /// IEnumerable<TestInfo> GetTests();
        /// TestDetailedInfo GetTestInfo(int testId); }
        /// </summary>
        private IProfTestInfoProvider profTestInfoProvider;

      
    public TestInfoController(IProfTestInfoProvider profTestInfoProvider)
        {
            this.profTestInfoProvider = profTestInfoProvider;
        }

        /// <summary>
        ///  This method retrieves the list of available tests ("testId", "testName").
        /// </summary>
         // GET: api/profTest/list
        [Route("api/profTest/list")]
        [HttpGet("[action]")]
        public IEnumerable<ICommonTestInfo> GetTests()=> profTestInfoProvider.GetTests();



        /// <summary>
        ///  This method retrieves the test info ("testId", "testName", "description", 
        ///  "approximatedTime", "questionsCount") for a first test from the list (if any).
        /// </summary>
        // GET: api/profTest/{id}/info
        [Route("api/profTest/{id}/info")]
        [HttpGet("{id}")]
        public ICommonTestDetailedInfo GetTestInfo(int id) => profTestInfoProvider.GetTestInfo(id);
    }
}
