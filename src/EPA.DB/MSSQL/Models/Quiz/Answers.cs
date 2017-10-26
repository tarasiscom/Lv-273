using System.ComponentModel.DataAnnotations;
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

        public EPA.Common.DTO.ProfTest.Quiz.Answer ToCommon()
        {
            return Mapper.Map<EPA.Common.DTO.ProfTest.Quiz.Answer>(this);
        }
    }
}
