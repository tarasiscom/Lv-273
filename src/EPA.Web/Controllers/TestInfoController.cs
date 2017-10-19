using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces;
using EPA.Common.DTO;

namespace EPA.Web.Controllers
{
    [Route("api/profTest/list")]
    public class TestInfoController : Controller
    {
        private IProfTestInfoProvider profTestInfoProvider;
   
        public TestInfoController(IProfTestInfoProvider profTestInfoProvider)
        {
            this.profTestInfoProvider = profTestInfoProvider;
        }

        // GET: api/profTest/list
        [HttpGet("[action]")]
        public IEnumerable<CommonTestInfo> GetTests()
        {
            return profTestInfoProvider.GetTests();
        }

        // GET: api/profTest/{id}/info
        [HttpGet("{id}", Name = "GetTestInfo")]
        public CommonTestDetailedInfo GetTestInfo(int id)
        {
            return profTestInfoProvider.GetTestInfo(id);
        }
    }
}
