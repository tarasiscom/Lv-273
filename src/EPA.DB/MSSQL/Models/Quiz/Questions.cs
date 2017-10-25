using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto.CommonQuiz;
using AutoMapper;

namespace EPA.DB.MSSQL.Models.Quiz
{
    public class Questions
    {
        [Key]
        public int ID { get; set; }
        public string Question { get; set; }

        public List<Answers> Answer { get; set; }
        public TestDetailedInfo TestListID { get; set; }

        public CommonQuestions ToCommon()
        {
            return Mapper.Map<CommonQuestions>(this);
        }

    }
}
