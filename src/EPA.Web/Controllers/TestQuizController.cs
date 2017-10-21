using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces;
using EPA.Common.DTO;
using EPA.Common.dto.CommonQuiz;

namespace EPA.Web.Controllers
{
    //[Produces("application/json")]
    public class TestQuizController : Controller
    {
        private IAccessToQuestionsByNameTest profTestQuestionsProvider;

        public TestQuizController(IAccessToQuestionsByNameTest profTestQuestionsProvider)
        {
            this.profTestQuestionsProvider = profTestQuestionsProvider;
        }

        /// <summary>
        /// This method retrieves list of tests
        /// </summary>
        /// <returns>collection of tests</returns>
        [Route("/api/profTest")]
        public IEnumerable<CommonTestList> GetTestList() => profTestQuestionsProvider.GetTestList();

        /// <summary>
        /// This method retrieves list of questions
        /// </summary>
        /// <param name="testId">id of selected test</param> 
        /// <returns>collection of questions</returns>
        [Route("/api/profTest/{testId}/questions")]
        public IEnumerable<CommonQuestions> GetQuestions(int testId) => profTestQuestionsProvider.GetQuestionByListID(testId);


        /// <summary>
        /// This method retrieves list of answers
        /// </summary>
        /// <param name="questionId">id of question</param>
        /// <returns>collection of answers</returns>
        [Route("/api/profTest/{testId}/{id}/answers")]
        public IEnumerable<CommonAnswers> GetAnswers(int questionId) => profTestQuestionsProvider.GetAnswersByQuestId(questionId);
    }
}