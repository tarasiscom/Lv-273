using AutoMapper;

namespace EPA.BusinessLogic.Models
{
    public class UserAnswer
    {
        public int IdQuestion { get; set; }
        public int IdAnswer { get; set; }

        public Common.DTO.UserAnswer ToCommon()
        {
            return Mapper.Map<Common.DTO.UserAnswer>(this);
        }
    }
}
