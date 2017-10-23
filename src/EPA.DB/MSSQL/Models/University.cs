using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using AutoMapper;

namespace EPA.DB.MSSQL.Models
{
    class University
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Site { get; set; }

        public List<Facultaty> Facultaties { get; set; }

        static University()
        { Mapper.Initialize(cfg => cfg.CreateMap < University, CommonProfTestResult> ()); }
    }
}
