using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces.ProfTest;
using EPA.Common.DTO;

namespace EPA.Web.Controllers.ProfTest
{

    /// <summary>
    ///  API for Test and TestInfo draws
    /// </summary>
    public class TestController : Controller
    {
        private readonly ITestProvider testProvider;

        public TestController(ITestProvider testProvider)
        {
            this.testProvider = testProvider;
        }

        /// <summary>
        ///  This method retrives list of accessible tests 
        /// </summary>
        ///  <returns> collection of Tests </returns>
        [Route("api/profTest/list")]
        [HttpGet]
        public IEnumerable<Test> GetTests() => testProvider.GetTests();



        /// <summary>
        /// This method retrives more detailed information about specific Test
        /// </summary>
        /// <param name="id"> id of the Test </param>
        /// <returns> more detatiled test Information </returns>
        [Route("api/profTest/{id}/info")]
        [HttpGet("{id}")]
        public TestInfo GetTestInfo(int id) => testProvider.GetTestInfo(id);



        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">ID of the test, whose questions we need</param>
        /// <returns>Collection of questions</returns>
        [Route("/api/profTest/{testId}/questions")]
        [HttpGet("{testId}")]
        public IEnumerable<Question> GetQuestions(int testId)
        {
            return testProvider.GetQuestions(testId);

            /*
            List<Question> list = testProvider.GetQuestions(testId).ToList();

            list.ForEach((x) => x.Answers = this.GetAnswers(x.ID).ToList());

            return list;
            //*/
        }

        /// <summary>
        ///  This method retrives data about persons professional directory and list of specialities
        /// </summary>
        /// <param name="points">Amount of points achieved</param>
        /// <param name="id">ID of the test, whose results we need</param>
        /// <returns>  ProfTest's Result </returns>
        [Route("api/profTest/{id}/result")]
        [HttpPost]
        public Result GetResult(int id, [FromBody]int points) => testProvider.GetResult(points, id);


    }
}
