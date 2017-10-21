using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EPA.DB.MSSQL.Models.Quiz
{
    public class TestList
    {
        [Key]
        public int ID { get; set; }
        public string TestName { get; set; }

        public virtual List<Questions> Questions { get; set; }
    }
}
