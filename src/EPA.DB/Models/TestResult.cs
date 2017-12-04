using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class TestResult
    {
        [Key]
        public int Id { get; set; }

        public TestDetailedInfo TestDetailedInfo { get; set; }

        public User User { get; set; }

        public List<TestScore> TestScore { get; set; }
    }
}
