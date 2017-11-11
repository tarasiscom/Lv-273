using AutoMapper;

namespace EPA.BusinessLogic.Models
{
    public class GeneralDirection
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Common.DTO.GeneralDirection ToCommon()
        {
            return Mapper.Map<Common.DTO.GeneralDirection>(this);
        }
    }
}
