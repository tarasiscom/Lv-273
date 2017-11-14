using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }

        public string Text { get; set; }

        public Question Question { get; set; }

        public Common.DTO.Answer ToCommon()
        {
            return Mapper.Map<Common.DTO.Answer>(this);
        }
    }
}
