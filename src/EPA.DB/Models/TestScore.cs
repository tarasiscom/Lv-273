using EPA.Common.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPA.MSSQL.Models
{
    public class TestScore
    {
        [Key]
        public int Id { get; set; }

        public TestResult TestResult { get; set; }

        public GeneralDirection GeneralDirection { get; set; }

        public int Score { get; set; }
    }
}
