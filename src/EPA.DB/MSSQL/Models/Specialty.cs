using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.DB.MSSQL.Models
{
    class Specialty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // foreign keys
        public int Id_university { get; set; }
        public University University { get; set; }
        public int Id_direction { get; set; }
        public Direction Direction { get; set; }
    }
}
