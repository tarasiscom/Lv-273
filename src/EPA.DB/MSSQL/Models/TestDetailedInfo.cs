using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto;
using AutoMapper;

namespace EPA.DB.MSSQL.Models
{
    public class TestDetailedInfo: TestInfo
    {
        public string Description { get; set; }
        public int ApproximatedTime { get; set; }
        public int QuestionsCount { get; set; }
        public TestDetailedInfo():base()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TestDetailedInfo, CommonTestDetailedInfo>()
            .IncludeBase<TestInfo,CommonTestInfo>());
        }
        public new CommonTestDetailedInfo ToCommon()
        {
            return Mapper.Map<CommonTestDetailedInfo>(this);
        }
    }
}
