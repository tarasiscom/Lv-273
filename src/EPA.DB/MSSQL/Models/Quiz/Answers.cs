using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPA.DB.MSSQL.Models.Quiz
{
    public class Answers
    {
        [Key]
        public int ID { get; set; }
        public string Answer { get; set; }

        public Questions Qestion { get; set; }
    }
}
