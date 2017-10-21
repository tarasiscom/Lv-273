using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.DB.MSSQL.Models.Quiz
{
    public class Questions
    {
        [Key]
        public int ID { get; set; }
        public string Question { get; set; }

        public List<Answers> Answer { get; set; }
        public TestList TestListID { get; set; }
    }
}
