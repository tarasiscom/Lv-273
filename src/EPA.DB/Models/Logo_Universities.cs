using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class Logo_Universities
    {
        [Key]
        public int Id { get; set; }

        public byte[] Logo { get; set; }

        public Common.DTO.Logo_Universities ToCommon()
        {
            return Mapper.Map<Common.DTO.Logo_Universities>(this);
        }
    }
}
