using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using EPA.Common.dto;
using AutoMapper;

namespace EPA.DB.MSSQL.Models
{
    public class TestInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public TestInfo()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TestInfo, CommonTestInfo>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)));
        }
        public CommonTestInfo ToCommon()
        {
            return Mapper.Map<CommonTestInfo>(this);
        }
    }
}
