using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class Direction
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Specialty> Specialties { get; set; }

        public List<ProfDirection> ProfDirections { get; set; }

        public GeneralDirection GeneralDirection { get; set; }
    }
}
