using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.Interfaces;
using EPA.Common.DTO;

namespace EPA.DB.MSSQL.Models
{
    public class TestInfo : CommonTestInfo
    {
        [Key]
        public override int Id { get; set; }
        public override string Name { get; set; }
    }
}
