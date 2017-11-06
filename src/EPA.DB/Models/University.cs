﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class University
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int DistrictID { get; set; }

        public string Address { get; set; }

        public string Site { get; set; }

        public List<Specialty> Specialties { get; set; }
    }
}
