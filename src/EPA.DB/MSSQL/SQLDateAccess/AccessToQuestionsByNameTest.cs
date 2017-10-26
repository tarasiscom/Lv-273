using System.Collections.Generic;
using EPA.DB.MSSQL.Models;
using System.Linq;
using EPA.Common.Interfaces.ProfTest;


namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class AccessToQuestions : ITestQuizProvider
    {
        private EpaContext epaContext;

        public AccessToQuestions()
        {
            this.epaContext = new EpaContext();
        }

        public IEnumerable<EPA.Common.DTO.ProfTest.Quiz.Question> GetQuestions(int testId)
        {
            return this.epaContext.Questions.Where(v => v.TestListID.Id == testId).Select(it => it.ToCommon()).ToList();
        }

        public IEnumerable<EPA.Common.DTO.ProfTest.Quiz.Answer> GetAnswers(int questionId)
        {
            return this.epaContext.Answers.Where(td => td.Qestion.ID == questionId)
                    .Select(item => item.ToCommon()).ToList<EPA.Common.DTO.ProfTest.Quiz.Answer>();
        }
    }
}
