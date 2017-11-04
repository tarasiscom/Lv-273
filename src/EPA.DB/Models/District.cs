using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class District
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
