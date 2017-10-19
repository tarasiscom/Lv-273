using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto;

namespace EPA.DB.MSSQL.Models
{
    public class TestInfo:CommonTestInfo
    {
        [Key]
        public override int Id { get; set; }
        [Required]
        public override string Name { get; set; }
    }
}
