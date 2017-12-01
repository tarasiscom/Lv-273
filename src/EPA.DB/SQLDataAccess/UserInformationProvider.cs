using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EPA.Common.DTO;
using EPA.Common.DTO.UserProvider;
using EPA.Common.Interfaces;
using EPA.MSSQL.Calculations;
using EPA.MSSQL.Models;
using Microsoft.Extensions.Options;

namespace EPA.MSSQL.SQLDataAccess
{
    public class UserInformationProvider : IUserInformationProvider
    {
        private readonly EpaContext context;

        private readonly IOptions<ConstSettings> constValues;

        public UserInformationProvider(EpaContext context, IOptions<ConstSettings> constValues)
        {
            this.context = context;
            this.constValues = constValues;
        }

        public IEnumerable<Common.DTO.Specialty> GetFavoriteSpecialty(int page, string UserID)
        {
            var specialties = from user in this.context.User_Specialty
                              where user.User.Id == UserID
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
            return specialties.Skip(page * constValues.Value.CountForPage).Take(constValues.Value.CountForPage).ToList();
        }

        public UserPersonalInfo GetPersonalInfo(string UserID)
        {
            return this.context.Users.Where(x => x.Id == UserID).First().ToPersonalInfo();
        }

        public void AddSpecialtyToFavorite(string UserId, int UserID)
        {
            User_Specialty add = new User_Specialty();
            add.Specialty.Id = UserID;
            add.User.Id = UserId;
            this.context.User_Specialty.Contains(add);
            var rez = this.context.User_Specialty.Where(x => x.Specialty.Id == UserID && x.User.Id == UserId).First();
            if (rez != null) return;
            this.context.User_Specialty.Add(add);
            this.context.SaveChanges();
        }

        public void RemoveSpecialtyFromFavorite(string userId, int specialtyId)
        {
            User_Specialty remove = this.context.User_Specialty.First(x => x.Specialty.Id == specialtyId && x.User.Id == userId);
            this.context.User_Specialty.Remove(remove);
            this.context.SaveChanges();
        }

        public Count CountOfFavoriteSpecialtys(string UserID)
        {
            Count result = new Count();
            result.AllElements = this.context.User_Specialty.Select(x => x.User.Id == UserID).Count();
            result.ForOnePage = constValues.Value.CountForPage;
            return result;
        }
    }
}
