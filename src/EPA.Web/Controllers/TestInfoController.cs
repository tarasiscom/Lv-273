using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces;
using EPA.Common.DTO;
using EPA.Common.dto;

namespace EPA.Web.Controllers
{
    public class TestInfoController : Controller
    {
        private IProfTestInfoProvider profTestInfoProvider;

        public TestInfoController(IProfTestInfoProvider profTestInfoProvider)
        {
            this.profTestInfoProvider = profTestInfoProvider;
        }

        /// <summary>
        ///  This method retrieves the list of available ProfTests
        /// </summary>
         // GET: api/profTest/list
        [Route("api/profTest/list")]
        public IEnumerable<ICommonTestInfo> GetTests() => profTestInfoProvider.GetTests();


        /// <summary>
        ///  This method retrieves the testInfo for current ProfTest
        ///  <param>id of selected ProfTest</param>
        /// </summary>
        // GET: api/profTest/{id}/info
        [Route("api/profTest/{id}/info")]
        [HttpGet("{id}")]
        public ICommonTestDetailedInfo GetTestInfo(int id) => profTestInfoProvider.GetTestInfo(id);



        [Route("api/profTest/{id}/questions")]
        [HttpGet("{id}")]
        public TestQuestionsCall GetQuestions(int id)
        {
            //это все нужно заменить нормальным апи
            return new TestQuestionsCall
            {
                id = id,
                que = new TestQuestions[] {
                    new TestQuestions{ id = 1, name = "que1" , ans = new TestAnswers[] {
                        new TestAnswers{ name= "ans1", pts = 1 },new TestAnswers{ name= "ans2", pts = 2 },new TestAnswers{ name= "ans3", pts = 3 } } },
                    
                    new TestQuestions{ id = 2, name = "que2",  ans = new TestAnswers[] {
                        new TestAnswers{ name= "ans1", pts = 1 },new TestAnswers{ name= "ans2", pts = 2 },new TestAnswers{ name= "ans3", pts = 3 } } }
                }
            };
        }

        public class TestQuestionsCall
        {
            public int id;
            public TestQuestions[] que;
        }


        public class TestQuestions
        {
            public int id;
            public string name;
            public TestAnswers[] ans;
        }


        public class TestAnswers
        {
            public int pts;
            public string name;
        }
    }
}
