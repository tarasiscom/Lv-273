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
    public class TestController : Controller
    {
        private IProfTestInfoProvider profTestInfoProvider;
   
        public TestController(IProfTestInfoProvider profTestInfoProvider)
        {
            this.profTestInfoProvider = profTestInfoProvider;
        }
        // GET: api/profTest/list
        [HttpGet("[action]")]
        public IEnumerable<CommonTestInfo> GetTests()
        {

            return profTestInfoProvider.GetDate().DateValue;
           // return DateTime.Now;
        }

       
    }
}
