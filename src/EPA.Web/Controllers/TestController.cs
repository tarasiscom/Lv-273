using EPA.Common.Interfaces;
using EPA.Common.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EPA.Web.Controllers.ProfTest
{
    /// <summary>
    ///  API for Test and TestInfo draws
    /// </summary>
    public class TestController : Controller
    {
        private readonly ITestProvider testProvider;
        private readonly IUserAnswersProdiver userAnswersProdiver;

        public TestController(ITestProvider testProvider, IUserAnswersProdiver userAnswersProdiver)
        {
            this.testProvider = testProvider;
            this.userAnswersProdiver = userAnswersProdiver;
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
        [Route("api/profTest/{id}/info")]
        [HttpGet]
        public TestInfo GetTestInfo(int id) => this.testProvider.GetTestInfo(id);

        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">ID of the test, whose questions we need</param>
        /// <returns>Collection of questions</returns>
        [Route("/api/profTest/{testId}/questions")]
        [HttpGet]
        public IEnumerable<Question> GetQuestions(int testId) => this.testProvider.GetQuestions(testId);

        /// <summary>
        /// This method retrieves general directions with scores based on user answers
        /// </summary>
        /// <param name="listansw">Take list of objects, that contains id question and answer number</param>
        /// <returns>List of general directions with scores</returns>
        [Route("api/profTest/{testId}/result")]
        [HttpPost]
        public IEnumerable<DirectionScores> GetDirection_Score([FromBody]List<UserAnswer> listansw)
                 => this.userAnswersProdiver.CalculateScores(listansw);
    }
}
