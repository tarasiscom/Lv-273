using System;
using System.Collections.Generic;
using System.Text;
using EPA.DB.MSSQL.Models;
using System.Linq;
using EPA.Common.Interfaces;
using EPA.Common.dto;
using EPA.DB.MSSQL.Models.Quiz;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    class AccessToQuestions
    {
        EpaContext epaContext;

        public AccessToQuestions()
        {
            epaContext = new EpaContext();
        }

        public List<TestList> GetTestList()
        {
            return epaContext.TestLists.ToList<TestList>();
            
        }

        public IEnumerable<Questions> GetQuestionByListID(int testId)
        {
            return epaContext.Questions.Where(td => td.TestListID.ID == testId).ToList<Questions>();
        }

        public IEnumerable<Answers> GetAnswersByQuestId(int questionId)
        {
            return epaContext.Answers.Where(td => td.Qestion.ID == questionId).ToList<Answers>();
        }
    }
}
