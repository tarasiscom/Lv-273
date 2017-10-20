using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto;

namespace EPA.DB.MSSQL.Models
{
    public class TestDetailedInfo: TestInfo, ICommonTestDetailedInfo
    {
        public string Description { get; }
        public int ApproximatedTime { get; }
        public int QuestionsCount { get; }
    }
}
