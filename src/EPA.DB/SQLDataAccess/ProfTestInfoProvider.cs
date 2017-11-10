using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Models;
using Microsoft.Extensions.Options;

namespace EPA.MSSQL.SQLDataAccess
{
    public class ProfTestInfoProvider : ITestProvider
    {
        private readonly IOptions<ConstSettings> constValues;

        private readonly EpaContext context;

        public ProfTestInfoProvider(IOptions<ConstSettings> constSettings, EpaContext cont)
        {
            this.context = cont;
            this.constValues = constSettings;
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
                               join d in this.context.Districts on u.DistrictID equals d.Id
                               where s.Direction.Name == direction
                               select new Common.DTO.Specialty()
                               {
                                   Name = s.Name,
                                   Address = u.Address,
                                   District = d.Name,
                                   Site = u.Site,
                                   University = u.Name
                               }).Distinct().Take(this.constValues.Value.NumberOfUniversities).ToList()
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

        public GeneralDirectionResult[] GetResults(int testId) {
            Common.DTO.GeneralDirection[] t = new Common.DTO.GeneralDirection[6];
            t[0] = new Common.DTO.GeneralDirection { ID = 0, Name = "One" };
            t[1] = new Common.DTO.GeneralDirection { ID = 1, Name = "Two" };
            t[2] = new Common.DTO.GeneralDirection { ID = 2, Name = "Three" };
            t[3] = new Common.DTO.GeneralDirection { ID = 3, Name = "Four" };
            t[4] = new Common.DTO.GeneralDirection { ID = 4, Name = "Five" };
            t[5] = new Common.DTO.GeneralDirection { ID = 5, Name = "Six" };

            GeneralDirectionResult[] temp = new GeneralDirectionResult[6];
            temp[0] = new GeneralDirectionResult { generaldirection = t[0], Score = 5 };
            temp[1] = new GeneralDirectionResult { generaldirection = t[1], Score = 6 };
            temp[2] = new GeneralDirectionResult { generaldirection = t[2], Score = 11 };
            temp[3] = new GeneralDirectionResult { generaldirection = t[3], Score = 3 };
            temp[4] = new GeneralDirectionResult { generaldirection = t[4], Score = 4 };
            temp[5] = new GeneralDirectionResult { generaldirection = t[5], Score = 9 };
            return temp;
        }
    }
}
