﻿using System;
using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Models;

namespace EPA.MSSQL.SQLDataAccess
{
    public class ProfTestInfoProvider : ITestProvider
    {
        private readonly EpaContext context;
        private const int NumberOfUniversities = 5;
 
        public ProfTestInfoProvider()
        {
            this.context = new EpaContext();
        }

        public TestInfo GetTestInfo(int testId)
        {
            return this.context.Tests.Find(testId).ToCommon();
        }

        public IEnumerable<Test> GetTests()
        {
            return this.context.Tests.Select(item => item.ToCommon());
        }

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

        public IEnumerable<Common.DTO.Question> GetQuestions(int testId)
        {
            return this.context.Questions.Where(v => v.TestListID.Id == testId)
                                            .Select(it => it.ToCommon()).ToList();
         //   this.context.Answers.Where(td => td.Qestion.ID == questionId)
         //                              .Select(item => item.ToCommon())
         //                              .ToList<EPA.Common.DTO.Answer>();
        }
    }
}
