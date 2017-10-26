using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces.ProfTest;
using EPA.Common.DTO.ProfTest.Quiz;

namespace EPA.Web.Controllers.ProfTest
{

    /// <summary>
    ///  API for Question and Answer draws
    /// </summary>
    public class TestQuizController : Controller
    {
        private ITestQuizProvider profTestQuestionsProvider;

        public TestQuizController(ITestQuizProvider profTestQuestionsProvider)
        {
            this.profTestQuestionsProvider = profTestQuestionsProvider;
        }

        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">ID of the test, whose questions we need</param>
        /// <returns>Collection of questions</returns>
        [Route("/api/profTest/{testId}/questions")]
        [HttpGet("{testId}")]
        public IEnumerable<Question> GetQuestions(int testId)
        {
            List<Question> list = profTestQuestionsProvider.GetQuestions(testId).ToList();

            list.ForEach((x) => x.Answers = this.GetAnswers(x.ID).ToList());
            
            return list;

        }



        /// <summary>
        /// This method returns a collection of answers for a specific question
        /// </summary>
        /// <param name="questionId">ID of the question, whose answers we need</param>
        /// <returns>Collection of answers</returns>
        public IEnumerable<Answer> GetAnswers(int questionId) => profTestQuestionsProvider.GetAnswers(questionId);
    }
}