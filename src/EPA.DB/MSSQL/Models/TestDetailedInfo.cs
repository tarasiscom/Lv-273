using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto;

namespace EPA.DB.MSSQL.Models
{
    public class TestDetailedInfo: CommonTestDetailedInfo
    {
        public override string Description { get; set; }
        public override int ApproximatedTime { get; set; }
        public override int QuestionsCount { get; set; }
    }
}
