using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.Interfaces;
using EPA.Common.DTO;

namespace EPA.DB.MSSQL.Models
{
    public class Date : CommonDate
    {
        [Key]
        public override int Id { get; set; }
        public override DateTime DateValue { get; set; }
    }
}
