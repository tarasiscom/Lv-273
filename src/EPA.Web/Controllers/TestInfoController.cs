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
        private IProfTestInfoProvider profTestInfoProvider;
 
        public TestInfoController(IProfTestInfoProvider profTestInfoProvider)
            {
                this.profTestInfoProvider = profTestInfoProvider;
            }

            /// <summary>
            ///  This method retrieves the list of available ProfTests
            /// </summary>
             // GET: api/profTest/list
            [Route("api/profTest/list")]
            public IEnumerable<ICommonTestInfo> GetTests() => profTestInfoProvider.GetTests();

            /// <summary>
            ///  This method retrieves the testInfo for current ProfTest
            ///  <param>id of selected ProfTest</param>
            /// </summary>
            // GET: api/profTest/{id}/info
            [Route("api/profTest/{id}/info")]
            [HttpGet("{id}")]
            public ICommonTestDetailedInfo GetTestInfo(int id) => profTestInfoProvider.GetTestInfo(id);
    }
}
