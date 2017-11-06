﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
﻿using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumApplication { get; set; }

        public int NumEnrolled { get; set; }

        // foreign keys
        public University University { get; set; }

        public Direction Direction { get; set; }

        public List<Specialty_Subject> SpecialtySubject { get; set; }
    }
}
