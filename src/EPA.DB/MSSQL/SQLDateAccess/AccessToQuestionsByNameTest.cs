using System;
using System.Collections.Generic;
using System.Text;
using EPA.DB.MSSQL.Models;
using System.Linq;
using EPA.Common.Interfaces;
using EPA.DB.MSSQL.Models.Quiz;
using EPA.Common.dto.CommonQuiz;
using AutoMapper;


namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class AccessToQuestions:IAccessToQuestionsByNameTest
    {
        EpaContext epaContext;

        public AccessToQuestions()
        {
            epaContext = new EpaContext();
        }

        public IEnumerable<CommonQuestions> GetQuestionByListID(int testId)
        {
            var y = epaContext.Questions.Where(v => v.TestListID.Id == testId).ToList();

            var x = epaContext.Questions.Where(v => v.TestListID.Id == testId).Select(it => it.ToCommon()).ToList();
               /* epaContext.Questions.Where(td => td.TestListID.Id == testId).
                Select(item=>item.ToCommon()).ToList<CommonQuestions>();*/
            return x;
        }

        public IEnumerable<CommonAnswers> GetAnswersByQuestId(int questionId)
        {
            return epaContext.Answers.Where(td => td.Qestion.ID == questionId).
                Select(item => item.ToCommon()).ToList<CommonAnswers>();
        }
    }
}
