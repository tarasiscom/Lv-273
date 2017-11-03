using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
    }
}
