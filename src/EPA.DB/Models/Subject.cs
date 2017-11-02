using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EPA.MSSQL.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
