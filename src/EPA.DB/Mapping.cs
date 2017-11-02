﻿using EPA.MSSQL.Models;

    namespace EPA.MSSQL
{
    public class Mapping
    {
        public void Create()
        {
            AutoMapper.Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<Question, EPA.Common.DTO.Question>();
                        cfg.CreateMap<Answer, EPA.Common.DTO.Answer>();
                        cfg.CreateMap<TestDetailedInfo, EPA.Common.DTO.TestInfo>();
                    });
        }
    }
}