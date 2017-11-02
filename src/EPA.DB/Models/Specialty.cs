using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        // foreign keys
        public University University { get; set; }

        public Direction Direction { get; set; }

        public Common.DTO.Specialty ToCommon()
        {
            return Mapper.Map<Common.DTO.Specialty>(this);
        }
    }
}
