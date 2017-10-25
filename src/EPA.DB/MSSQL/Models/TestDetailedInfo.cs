using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto;
using AutoMapper;

namespace EPA.DB.MSSQL.Models
{
    public class TestDetailedInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApproximatedTime { get; set; }
        public int QuestionsCount { get; set; }
        
        public List<Quiz.Questions> Questions { get; set; } 

        //List<Quiz.Questions> Questions { get; set; }
        List<ProfDirection> ProfDirections { get; set; }

        static TestDetailedInfo()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TestDetailedInfo, CommonTestDetailedInfo>());
        }

        public CommonTestDetailedInfo ToCommon()
        {
            return Mapper.Map<CommonTestDetailedInfo>(this);
        }
    }
}
