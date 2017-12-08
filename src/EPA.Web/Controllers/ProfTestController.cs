using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Test and TestInfo data draws
    /// </summary>
    public class ProfTestController : Controller
    {
        private readonly ITestProvider testProvider;
        private readonly IScoreProdiver answersProdiver;

        public ProfTestController(ITestProvider testProvider, IScoreProdiver answersProdiver)
        {
            this.testProvider = testProvider;
            this.answersProdiver = answersProdiver;
        }

        /// <summary>
        /// This method retrives list of accessible tests
        /// </summary>
        /// <returns> collection of Tests </returns>
        [Route("api/profTest/list")]
        [HttpGet]
        public IEnumerable<Test> GetTests() => this.testProvider.GetTests();

        /// <summary>
        /// This method retrives more detailed information about specific test
        /// </summary>
        /// <param name="id"> Id of the test </param>
        /// <returns> More detatiled test information </returns>
        [Route("api/profTest/{id:int}/info")]
        [HttpGet]
        public TestInfo GetTestInfo(int id) => this.testProvider.GetTestInfo(id);

        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">Id of the test</param>
        /// <returns>Collection of questions</returns>
        [Route("api/profTest/{testId:int}/questions")]
        [HttpGet]
        public IEnumerable<Question> GetQuestions(int testId)
        {
            return this.testProvider.GetQuestions(testId);
        }

        /// <summary>
        /// This method retrieves general directions with scores based on user answers
        /// </summary>
        /// <param name="listAnswers">list of user answers</param>
        /// <param name="testId">Id of the test</param>
        /// <returns>List of general directions with scores</returns>
        [Route("api/profTest/{testId:int}/result")]
        [HttpPost]
        public IEnumerable<DirectionScores> GetDirectionsScore([FromBody]List<UserAnswer> listAnswers, int testId)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var a = this.GetUserId(this.User);
                this.testProvider.AddTestResult(this.answersProdiver.CalculateScores(listAnswers), a, testId);
            }

            return this.answersProdiver.CalculateScores(listAnswers);
        }

        /// <summary>
        /// This method gives user Id. / Dublicate of UserController method
        /// </summary>
        /// <param name="principal">Claim Principal</param>
        /// <returns>User ID</returns>
        public string GetUserId(ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
