﻿using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPA.MSSQL.Models
{
    public class University
    {
        [Key]
        public int Id { get; set; }
        
        // foreign keys
        public District District { get; set; }

       // public byte[] Logo { get; set; }

        public Logo_Universities Logo_Universities { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Site { get; set; }

        public int Rating { get; set; }

        public List<Specialty> Specialties { get; set; }

        public Common.DTO.University ToCommon()
        {
            return Mapper.Map<Common.DTO.University>(this);
        }
    }
}
