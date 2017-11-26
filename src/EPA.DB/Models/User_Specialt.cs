using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class User_Specialty
    {
        [Key]
        public int Id { get; set; }

        public Specialty Specialty { get; set; }

        public User User { get; set; }
}
}
