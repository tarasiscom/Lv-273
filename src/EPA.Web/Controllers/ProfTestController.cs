﻿using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EPA.Web.Controllers
{
    /// <summary>
    ///  API for Test and TestInfo draws
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

        /// <summary>
        /// This method retrieves general directions with scores based on user answers
        /// </summary>
        /// <param name="listAnswers">Take list of objects, that contains id question and answer number</param>
        /// <returns>List of general directions with scores</returns>
        [Route("api/profTest/result")]
        [HttpPost]
        public IEnumerable<DirectionScores> GetDirectionsScore([FromBody]List<UserAnswer> listAnswers)
        {
            return this.answersProdiver.CalculateScores(listAnswers);
        }
    }
}
