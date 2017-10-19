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
        public string Description { get; set; }
        public int ApproximatedTime { get; set; }
        public int QuestionsCount { get; set; }
    }
}
