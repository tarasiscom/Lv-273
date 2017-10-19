using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.Interfaces;
using EPA.Common.DTO;

namespace EPA.DB.MSSQL.Models
{
    public class TestDetailedInfo: CommonTestDetailedInfo
    {
        [Key]
        public override string Description { get; set; }
        public override int ApproximatedTime { get; set; }
        public override int QuestionsCount { get; set; }
    }
}
