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
        [HttpGet("{testId}")]
        public IEnumerable<CommonQuestions> GetQuestions(int testId)
        {

            return new[] { new CommonQuestions { ID = 1, Question="que1",
                Answer = new List<CommonAnswers>{ new CommonAnswers() { Point=1, Answer="ans1" },
                    new CommonAnswers() { Point = 2, Answer = "ans2" }, new CommonAnswers() { Point = 3, Answer = "ans3" }} },

                           new CommonQuestions { ID = 2, Question="que2",
                Answer = new List<CommonAnswers>{ new CommonAnswers() { Point=1, Answer="ans1" },
                    new CommonAnswers() { Point = 2, Answer = "ans2" }, new CommonAnswers() { Point = 3, Answer = "ans3" }} },

                           new CommonQuestions { ID = 3, Question="que3",
                Answer = new List<CommonAnswers>{ new CommonAnswers() { Point=1, Answer="ans1" },
                    new CommonAnswers() { Point = 2, Answer = "ans2" }, new CommonAnswers() { Point = 3, Answer = "ans3" }} },

                           new CommonQuestions { ID = 4, Question="que4",
                Answer = new List<CommonAnswers>{ new CommonAnswers() { Point=1, Answer="ans1" },
                    new CommonAnswers() { Point = 2, Answer = "ans2" }, new CommonAnswers() { Point = 3, Answer = "ans3" }} },

            };

            /*
            List<CommonQuestions> list =  profTestQuestionsProvider.GetQuestionByListID(testId).ToList();

            
            foreach(var v in list)
            {
                v.Answer = this.GetAnswers(v.ID).ToList();
            }

            return list;*/

        }


        /// <summary>
        /// This method retrieves list of answers
        /// </summary>
        /// <param name="questionId">id of question</param>
        /// <returns>collection of answers</returns>
        [Route("/api/profTest/{testId}/{id}/answers")]
        public IEnumerable<CommonAnswers> GetAnswers(int questionId) => profTestQuestionsProvider.GetAnswersByQuestId(questionId);
    }
}