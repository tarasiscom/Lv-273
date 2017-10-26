using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.DB.MSSQL.Models
{
    public class ProfDirection
    {
        [Key]
        public int Id { get; set; }

        public int MinPoint { get; set; }

        public int MaxPoint { get; set; }

        // foreign keys
        public TestDetailedInfo TestDetailedInfo { get; set; }

        public Direction Direction { get; set; }
    }
}
