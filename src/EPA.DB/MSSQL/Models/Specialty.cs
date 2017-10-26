using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.DB.MSSQL.Models
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        // foreign keys

        public University University { get; set; }

        public Direction Direction { get; set; }
    }
}
