﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPA.MSSQL.Models
{
    public class GeneralDirection
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Direction> Directions { get; set; }

        public Common.DTO.GeneralDirection ToCommon()
        {
            return Mapper.Map<Common.DTO.GeneralDirection>(this);
        }
    }
}
