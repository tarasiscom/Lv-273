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

        private readonly RatingProvider ratingProvider;

        public UserInformationProvider(EpaContext context, IOptions<ConstSettings> constValues)
        {
            this.context = context;
            this.constValues = constValues;
            ratingProvider = new RatingProvider(constValues.Value.KoefOfNumApplication);
        }

        public IEnumerable<Common.DTO.Specialty> GetFavoriteSpecialty( string userId, int page)
        {
            var specialties = from user in this.context.User_Specialty
                              where user.User.Id == userId
                              join special in this.context.Specialties on user.Specialty.Id equals special.Id
                              join univer in this.context.Universities on special.University.Id equals univer.Id
                              join d in this.context.Districts on univer.District.Id equals d.Id
                              orderby ratingProvider.GetRating(univer.Rating, special.NumApplication, special.NumEnrolled) descending
                              select new Common.DTO.Specialty()
                              {
                                  Id = special.Id,
                                  Name = special.Name,
                                  Address = univer.Address,
                                  District = d.Name,
                                  Site = univer.Site,
                                  University = univer.Name,
                                  Subjects = (from ss in this.context.Specialty_Subjects
                                              where ss.Specialty.Id == special.Id
                                              select ss.Subject.ToCommon()).ToList(),
                                  isFavorite = (from us in this.context.User_Specialty
                                                where us.User.Id == userId && us.Specialty.Id == special.Id
                                                select us.Id).Any()

                              };
            return specialties.Skip(page * constValues.Value.CountForPage).Take(constValues.Value.CountForPage).ToList();
        }

        public UserPersonalInfo GetPersonalInfo(string UserID)
        {
            var y = (from user in this.context.Users
                     where user.Id == UserID
                     join district in this.context.Districts on user.District.Id equals district.Id
                     select new UserPersonalInfo()
                     {
                         District = district.Name,
                         Email = user.Email,
                         FirstName = user.FirstName,
                         Surname = user.Surname,
                         Phone = user.PhoneNumber
                     }).ToList().First();

            //return this.context.Users.Where(x => x.Id == UserID).First().ToPersonalInfo();
            return y;
        }

        public IEnumerable<Test> GetTestResults(string userId)
        {/*
            from tr in this.context.
            where user.Id == UserID
            join district in this.context.Districts on user.District.Id equals district.Id
            select new UserPersonalInfo()
            {
                District = district.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Phone = user.PhoneNumber
            }).ToList()*/
            return new List<Test>(){ new Test() };
        }

        public bool AddSpecialtyToFavorite(string UserId, int specialtyId)
        {
            
            User_Specialty add = new User_Specialty();
            add.Specialty = this.context.Specialties.Where(x => x.Id == specialtyId).First();
            add.User = this.context.Users.Where(x => x.Id == UserId).First();
            //this.context.User_Specialty.Contains(add);
            var rez = this.context.User_Specialty.Where(x => x.Specialty.Id == specialtyId && x.User.Id == UserId).FirstOrDefault();
            if (rez != null) return false;
            this.context.User_Specialty.Add(add);
            this.context.SaveChanges();
            return true;
        }

        public bool RemoveSpecialtyFromFavorite(string userId, int specialtyId)
        {
            User_Specialty remove = this.context.User_Specialty.First(x => x.Specialty.Id == specialtyId && x.User.Id == userId);
            //this.context.User_Specialty.Remove(remove).;
            this.context.SaveChanges();
            return true;
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
