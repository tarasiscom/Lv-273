using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto;

namespace EPA.DB.MSSQL.Models
{
    public class TestDetailedInfo: TestInfo, ICommonTestDetailedInfo
    {
        public string Description { get; set; }
        public int ApproximatedTime { get; set; }
        public int QuestionsCount { get; set; }
    }
}
