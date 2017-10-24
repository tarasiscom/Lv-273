using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.DB.MSSQL.Models
{
    public class Direction
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Specialty> Specialties { get; set; }
        public List<ProfDirection> ProfDirections { get; set; }
    }
}
