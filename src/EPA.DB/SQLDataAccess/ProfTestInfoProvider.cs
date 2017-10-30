using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Models;

namespace EPA.MSSQL.SQLDataAccess
{
    public class ProfTestInfoProvider : ITestProvider
    {
        private const int NumberOfUniversities = 5;
        private readonly EpaContext context;

        public ProfTestInfoProvider()
        {
            this.context = new EpaContext();
        }

        public TestInfo GetTestInfo(int testId) => this.context.Tests.Find(testId).ToCommon();

        public IEnumerable<Test> GetTests() => this.context.Tests.Select(item => item.ToCommon());

        public Result GetResult(int points, int testId)
        {
            string direction = (from d in this.context.Directions
                                join pd in this.context.ProfDirections on d.Id equals pd.Direction.Id
                                where points >= pd.MinPoint && pd.MaxPoint > points
                                select d.Name).FirstOrDefault();
            return new Result()
            {
                ProfDirection = direction,
                Specialties = (from s in this.context.Specialties
                               join u in this.context.Universities on s.University.Id equals u.Id
                               where s.Direction.Name == direction
                               select new Common.DTO.Specialty()
                               {
                                   Name = s.Name,
                                   Address = u.Address,
                                   District = u.District,
                                   Site = u.Site,
                                   University = u.Name
                               }).Take(NumberOfUniversities).ToList()
            };
        }

        public IEnumerable<Common.DTO.Question> GetQuestions(int testId) => this.context.Questions
                                    .Where(q => q.Test.Id == testId)
                                    .Select(res => new Models.Question
                                    {
                                        ID = res.ID,
                                        Test = res.Test,
                                        Text = res.Text,
                                        Answers = this.context.Answers
                                                                    .Where(answ => answ.Question.ID == res.ID)
                                                                    .ToList()
                                    }.ToCommon());
    }
}
