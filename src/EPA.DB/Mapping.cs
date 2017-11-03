using EPA.MSSQL.Models;

    namespace EPA.MSSQL
{
    public class Mapping
    {
        public void Create()
        {
            AutoMapper.Mapper.Initialize(
                    cfg =>
                    {
                        cfg.CreateMap<Question, Common.DTO.Question>();
                        cfg.CreateMap<Answer, Common.DTO.Answer>();
                        cfg.CreateMap<TestDetailedInfo, Common.DTO.TestInfo>();
                    });
        }
    }
}
