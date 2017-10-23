using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto.CommonQuiz;
using AutoMapper;

namespace EPA.DB.MSSQL.Models.Quiz
{
    public class TestList
    {
        [Key]
        public int ID { get; set; }
        public string TestName { get; set; }

        public virtual List<Questions> Questions { get; set; }

        static TestList()
        { Mapper.Initialize(cfg => cfg.CreateMap<TestList, CommonTestList>()); }

        public CommonTestList ToCommon()
        {
            return Mapper.Map<CommonTestList>(this);
        }
    }
}
