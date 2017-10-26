using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EPA.Common.DTO;

namespace EPA.DB.MSSQL.Models.Quiz
{
    public class Question
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Answers> Answers { get; set; }
        public TestDetailedInfo TestListID { get; set; }

        public Common.DTO.ProfTest.Quiz.Question ToCommon()
        {
            return Mapper.Map<Common.DTO.ProfTest.Quiz.Question>(this);
        }

    }
}
