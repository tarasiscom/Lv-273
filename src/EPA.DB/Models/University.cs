using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class University
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public District District { get; set; }

        public string Address { get; set; }

        public string Site { get; set; }

        public int Rating { get; set; }

        public List<Specialty> Specialties { get; set; }

        public byte[] Logo { get; set; }

        public Common.DTO.UniversityInfo ToCommon()
        {
            return Mapper.Map<Common.DTO.UniversityInfo>(this);
        }
    }
}
