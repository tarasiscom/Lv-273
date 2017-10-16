using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.DB.Models
{
    public class Date
    {
        [Key]
        public long Id { get; set; }
        public DateTime DateValue { get; set; }
    }
}
