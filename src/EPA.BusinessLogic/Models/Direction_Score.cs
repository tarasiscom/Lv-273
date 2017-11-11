using AutoMapper;
using EPA.BusinessLogic.Models;

namespace EPA.BusinessLogic.Models
{
    public class Direction_Score
    {
        public GeneralDirection GeneralDir { get; set; }
        public int Score { get; set; }

        public Common.DTO.Direction_Score ToCommon()
        {
            return Mapper.Map<Common.DTO.Direction_Score>(this);
        }
    }
}
