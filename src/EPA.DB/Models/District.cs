using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class District
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> User { get; set; }

        public Common.DTO.District ToCommon()
        {
            return Mapper.Map<Common.DTO.District>(this);
        }
    }
}
