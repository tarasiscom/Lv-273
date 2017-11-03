using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class Specialty_Subject
    {
        [Key]
        public int Id { get; set; }

        public Specialty Specialty { get; set; }

        public Subject Subject { get; set; }
    }
}
