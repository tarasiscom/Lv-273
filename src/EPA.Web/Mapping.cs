namespace EPA.Web
{
    public static class Mapping
    {
        public static void Create()
        {
            AutoMapper.Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<MSSQL.Models.Question, Common.DTO.Question>();
                        cfg.CreateMap<MSSQL.Models.Answer, Common.DTO.Answer>();
                        cfg.CreateMap<MSSQL.Models.TestDetailedInfo, Common.DTO.TestInfo>();
                        cfg.CreateMap<MSSQL.Models.Specialty, Common.DTO.Specialty>();
                        cfg.CreateMap<MSSQL.Models.GeneralDirection, Common.DTO.GeneralDirection>();
                        cfg.CreateMap<MSSQL.Models.Specialty, Common.DTO.Specialty>();
                        cfg.CreateMap<MSSQL.Models.Subject, Common.DTO.Subject>();
                        cfg.CreateMap<MSSQL.Models.District, Common.DTO.District>();
                        cfg.CreateMap<MSSQL.Models.University, Common.DTO.University>();
                        cfg.CreateMap<MSSQL.Models.Logo_Universities, Common.DTO.Logo_Universities>();
                    });
        }
    }
}
