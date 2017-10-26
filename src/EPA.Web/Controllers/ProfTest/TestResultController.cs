using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces.ProfTest;
using EPA.Common.DTO.ProfTest.Quiz;

namespace EPA.Web.Controllers.ProfTest
{
    /// <summary>
    ///  API for Test Results draws
    /// </summary>
    public class TestResultController : Controller
    {
        private ITestResultProvider profTestResultProvider;

        public TestResultController( ITestResultProvider profTestResultProvider)
        {
            this.profTestResultProvider = profTestResultProvider;
        }

        /// <summary>
        ///  This method retrives data about persons professional directory and list of specialities
        ///  <param name="points">Amount of points achieved</param>
        ///  <param name="testId">ID of the test, whose results we need</param>
        ///  <returns>  ProfTest's Result </returns>
        /// </summary>
        [Route("api/profTest/{id}/result")]
        [HttpPost]
        public Result GetResult(int id, [FromBody]int points) => profTestResultProvider.GetResult(points, id);



    }
}
