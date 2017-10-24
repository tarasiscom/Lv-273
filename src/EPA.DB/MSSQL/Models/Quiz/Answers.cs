using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EPA.Common.dto.CommonQuiz;
using AutoMapper;

namespace EPA.DB.MSSQL.Models.Quiz
{
    public class Answers
    {
        [Key]
        public int ID { get; set; }
        public string Answer { get; set; }
        public int Point { get; set; }

        public Questions Qestion { get; set; }

        static Answers()
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<Answers, CommonAnswers>());
        }
        public CommonAnswers ToCommon()
        {
            return Mapper.Map<CommonAnswers>(this);
        }
    }
}
