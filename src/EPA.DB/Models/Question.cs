using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace EPA.MSSQL.Models
{
    public class Question
    {
        [Key]
        public int ID { get; set; }

        public string Text { get; set; }

        public List<Answer> Answers { get; set; }

        public TestDetailedInfo TestListID { get; set; }

        public Common.DTO.Question ToCommon()
        {
            return Mapper.Map<Common.DTO.Question>(this);
        }
    }
}
