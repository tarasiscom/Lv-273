using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace EPA.MSSQL.Models
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }

        public string Text { get; set; }

        public int Point { get; set; }

        public Question Question { get; set; }

        public EPA.Common.DTO.Answer ToCommon()
        {
            return Mapper.Map<EPA.Common.DTO.Answer>(this);
        }
    }
}
