using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using AutoMapper;

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

        public int NumApplication { get; set; }

        public int NumEnrolled { get; set; }

        public List<Specialty_Subject> SpecialtySubject { get; set; }

        public List<User_Specialty> UserSpecialt { get; set; }

        public Common.DTO.Specialty ToCommon()
        {
            return Mapper.Map<Common.DTO.Specialty>(this);
        }
    }
}
