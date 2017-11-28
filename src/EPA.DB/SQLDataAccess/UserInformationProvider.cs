using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EPA.Common.DTO;
using EPA.Common.DTO.UserProvider;
using EPA.Common.Interfaces;
using EPA.MSSQL.Calculations;

namespace EPA.MSSQL.SQLDataAccess
{
    public class UserInformationProvider : IUserInformationProvider
    {
        private readonly EpaContext context;

        public UserInformationProvider(EpaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Specialty> GetFavoriteSpecialty()
        {
            id = "0698a357-1e00-4c93-8c64-c9b262ff8b4e";
            var temp = from user in this.context.User_Specialty where user.User.Id == id
                       join special in this.context.Specialties on user.Specialty.Id equals special.Id
                       join univer in this.context.Universities on special.University.Id equals univer.Id
                       join d in this.context.Districts on univer.District.Id equals d.Id
                       orderby RatingProvider.GetRating(special.NumApplication, special.NumEnrolled) descending
                       select new Common.DTO.Specialty()
                       {
                           Name = special.Name,
                           Address = univer.Address,
                           District = d.Name,
                           Site = univer.Site,
                           University = univer.Name,
                           Subjects = (from ss in this.context.Specialty_Subjects
                                       where ss.Specialty.Id == special.Id
                                       select ss.Subject.ToCommon()).ToList()
                       };
            return temp.ToList();
        }

        string id;

        public UserPersonalInfo PersonalInfo()
        {
            UserPersonalInfo userPersonalInfo=new UserPersonalInfo();
            var userInfo = this.context.Users.Where(x => x.Id == id).ToList();
            userPersonalInfo.District = userInfo[0].District.Name;
            throw new NotImplementedException();
        }
    }
}
