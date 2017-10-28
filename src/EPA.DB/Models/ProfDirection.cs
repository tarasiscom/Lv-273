using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class ProfDirection
    {
        [Key]
        public int Id { get; set; }

        public int MinPoint { get; set; }

        public int MaxPoint { get; set; }

        // foreign keys
        public TestDetailedInfo Test { get; set; }

        public Direction Direction { get; set; }
    }
}
