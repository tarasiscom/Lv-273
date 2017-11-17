using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
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

        public TestInfo GetTestInfo(int id) => this.context.Tests.Find(id).ToCommon();

        public IEnumerable<Test> GetTests() => this.context.Tests.Select(item => item.ToCommon());

        public IEnumerable<Common.DTO.Question> GetQuestions(int testId)
        {

            var x = this.context.Questions
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
            if (x.Count() < 1) throw new System.ArgumentException("No matching data available");
            return x;
        }

        public IEnumerable<Common.DTO.GeneralDirection> GetDirectionsInfo() =>
                    this.context.GeneralDirections.Select(item => item.ToCommon());
    }
}
