using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces.ProfTest;
using EPA.Common.DTO.ProfTest;

namespace EPA.Web.Controllers.ProfTest
{

    /// <summary>
    ///  API for Test and TestInfo draws
    /// </summary>
    public class TestInfoController : Controller
    {
        private ITestProvider profTestInfoProvider;

        public TestInfoController(ITestProvider profTestInfoProvider)
        {
            this.profTestInfoProvider = profTestInfoProvider;
        }

        /// <summary>
        ///  This method retrives list of accessible tests 
        ///  <returns> collection of Tests </returns>
        /// </summary>
        [Route("api/profTest/list")]
        [HttpGet]
        public IEnumerable<Test> GetTests() => profTestInfoProvider.GetTests();



        /// <summary>
        ///  This method retrives more detailed information about specific Test
        ///  <param> id of the Test </param>
        ///  <returns> more detatiled test Information </returns>
        /// </summary>
        [Route("api/profTest/{id}/info")]
        [HttpGet("{id}")]
        public TestInfo GetTestInfo(int id) => profTestInfoProvider.GetTestInfo(id);
        


    }
}
