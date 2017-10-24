using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using AutoMapper;

namespace EPA.DB.MSSQL.Models
{
    public class University
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Site { get; set; }

        public List<Specialty> Specialties { get; set; }
    }
}
