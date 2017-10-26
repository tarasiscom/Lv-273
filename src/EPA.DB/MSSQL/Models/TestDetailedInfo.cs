﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.DTO.ProfTest;
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

        public List<ProfDirection> ProfDirections { get; set; }

        public TestInfo ToCommon()
        {
            return Mapper.Map<TestInfo>(this);
        }
    }
}
