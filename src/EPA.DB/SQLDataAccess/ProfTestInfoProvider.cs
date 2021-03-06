﻿using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using Microsoft.Extensions.Options;

namespace EPA.MSSQL.SQLDataAccess
{
    /// <summary>
    /// This class contains methods for obtaining test data from database
    /// </summary>
    public class ProfTestInfoProvider : ITestProvider
    {
        private readonly IOptions<ConstSettings> constValues;

        private readonly EpaContext context;

        public ProfTestInfoProvider(IOptions<ConstSettings> constSettings, EpaContext context)
        {
            this.context = context;
            this.constValues = constSettings;
        }

        /// <summary>
        /// This method retrieves more detailed information about specific test
        /// </summary>
        /// <param name="id"> Id of the test </param>
        /// <returns> More detatiled test information </returns>
        public TestInfo GetTestInfo(int id) => this.context.Tests.Find(id).ToCommon();

        /// <summary>
        /// This method retrieves collection of all tests
        /// </summary>
        /// <returns> Collection of tests </returns>
        public IEnumerable<Test> GetTests() => this.context.Tests.Select(item => item.ToCommon());

        /// <summary>
        /// This method returns a collection of questions for a specific test
        /// </summary>
        /// <param name="testId">ID of the test</param>
        /// <returns>Collection of questions</returns>
        public IEnumerable<Question> GetQuestions(int testId)
        {
            var questions = this.context.Questions
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

            if (questions.Count() < 1)
            {
                throw new System.ArgumentException("No matching data available");
            }

            return questions;
        }

        /// <summary>
        /// This method returns general directions
        /// </summary>
        /// <returns>Collection of general directions</returns>
        public IEnumerable<GeneralDirection> GetDirectionsInfo() =>
                    this.context.GeneralDirections.Select(item => item.ToCommon());

        /// <summary>
        /// This method saves test results for user
        /// </summary>
        /// <param name="list">Test Results</param>
        /// <param name="userId">User's ID</param>
        /// <param name="testId">Test for which results are beign saved</param>
        /// <returns>Logical flag that represents operation status</returns>
        public bool AddTestResult(List<DirectionScores> list, string userId, int testId)
        {
            var user = this.context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var testDetailIfo = this.context.Tests.Where(x => x.Id == testId).FirstOrDefault();
            var testScore = list.Select(
                x => new Models.TestScore()
                {
                    GeneralDirection = this.context.GeneralDirections.Where(y => y.Id == x.GeneralDir.Id).First(),
                    Score = x.Score
                }).ToList();
            Models.TestResult result = new Models.TestResult()
            {
                User = user,
                TestDetailedInfo = testDetailIfo,
                TestScore = testScore
            };
            var flag = this.context.TestResult.Where(x => x.TestDetailedInfo.Id == result.TestDetailedInfo.Id &&
            x.User.Id == result.User.Id).FirstOrDefault();
            if (flag != null)
            {
                this.context.TestResult.Remove(flag);
                this.context.SaveChanges();
            }

            this.context.TestResult.Add(result);
            this.context.SaveChanges();
            return false;
        }
    }
}
