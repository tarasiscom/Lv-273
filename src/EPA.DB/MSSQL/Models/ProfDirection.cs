using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.DB.MSSQL.Models
{
    class ProfDirection
    {
        [Key]
        public int Id { get; set; }
        public int MinPoint { get; set; }
        public int MaxPoint { get; set; }

        // foreign keys
        public int Id_test { get; set; }
        public TestInfo TestInfo { get; set; } // public Test і в Test додати List<Profdirection>
        public int Id_direction { get; set; }
        public Direction Direction { get; set; }
    }
}
