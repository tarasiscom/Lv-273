using EPA.Common.Interfaces;
using EPA.Common.DTO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Test and TestInfo draws
    /// </summary>
    public class TestController : Controller
    {
        private readonly ITestProvider testProvider;

        public TestController(ITestProvider testProvider, IAnswersProdiver answersProdiver)
        {
            this.testProvider = testProvider;
        }

        /// <summary>
        /// This method retrives list of accessible tests
        /// </summary>
        /// <returns> collection of Tests </returns>
        [Route("api/profTest/list")]
        [HttpGet]
        public IEnumerable<Test> GetTests() => this.testProvider.GetTests();

        /// <summary>
        /// This method retrives more detailed information about specific Test
        /// </summary>
        /// <param name="id"> id of the Test </param>
        /// <returns> more detatiled test Information </returns>
        [Route("api/profTest/{id:int}/info")]
        [HttpGet]
        public TestInfo GetTestInfo(int id) => this.testProvider.GetTestInfo(id);

        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">ID of the test, whose questions we need</param>
        /// <returns>Collection of questions</returns>
        [Route("api/profTest/{testId:int}/questions")]
        [HttpGet]
        public IEnumerable<Question> GetQuestions(int testId)
        {
            return this.testProvider.GetQuestions(testId);
        }

    }
}
