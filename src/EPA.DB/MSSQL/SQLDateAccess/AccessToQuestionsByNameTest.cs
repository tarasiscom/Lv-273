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

        public IEnumerable<CommonTestList> GetTestList()
        {
            
            List<TestList>tests=epaContext.TestLists.ToList<TestList>();
            List<CommonTestList> CommTests = new List<CommonTestList>();
            
            Mapper.Initialize(cfg =>cfg.CreateMap<TestList, CommonTestList>());
            if (tests.Count == 0)
            { return null;}

            foreach (TestList t in tests)
            {
                CommTests.Add(Mapper.Map<CommonTestList>(t)); 
            }
            
            return CommTests;
            
        }

        public IEnumerable<CommonQuestions> GetQuestionByListID(int testId)
        {
            List<Questions>quest=epaContext.Questions.Where(td => td.TestListID.ID == testId).ToList<Questions>();
            List<CommonQuestions> commQuest = new List<CommonQuestions>();

            Mapper.Initialize(cfg => cfg.CreateMap<Questions, CommonQuestions>());
            if (quest.Count == 0) { return null; }

            foreach (Questions q in quest)
                commQuest.Add(Mapper.Map<CommonQuestions>(q));
            return commQuest;
        }

        public IEnumerable<CommonAnswers> GetAnswersByQuestId(int questionId)
        {
            List<Answers>answer=epaContext.Answers.Where(td => td.Qestion.ID == questionId).ToList<Answers>();
            List<CommonAnswers> commAnswer = new List<CommonAnswers>();

            Mapper.Initialize(cfg => cfg.CreateMap<Answers, CommonAnswers>());
            if (answer.Count == 0) { return null; }

            foreach (Answers a in answer)
                commAnswer.Add(Mapper.Map<CommonAnswers>(a));
            return commAnswer;
        }
    }
}
