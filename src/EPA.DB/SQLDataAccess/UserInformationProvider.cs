using System;
using System.Linq;
using System.Collections.Generic;
using EPA.Common.DTO;
using EPA.Common.DTO.UserProvider;
using EPA.Common.Interfaces;
using EPA.MSSQL.Calculations;
using EPA.MSSQL.Models;
using Microsoft.Extensions.Options;

namespace EPA.MSSQL.SQLDataAccess
{
    /// <summary>
    /// This class obtain methods for getting user related data
    /// </summary>
    public class UserInformationProvider : IUserInformationProvider
    {
        private readonly EpaContext context;
        private readonly IOptions<ConstSettings> constValues;
        private readonly RatingProvider ratingProvider;

        public UserInformationProvider(EpaContext context, IOptions<ConstSettings> constValues)
        {
            this.context = context;
            this.constValues = constValues;
            this.ratingProvider = new RatingProvider(constValues.Value.KoefOfNumApplication);
        }

        /// <summary>
        /// This method retrieves user's favorite specialties
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="page">page iterator</param>
        /// <returns>Collection of favorite specialties</returns>
        public IEnumerable<Common.DTO.Specialty> GetFavoriteSpecialty(string userId, int page)
        {
            var specialties = from user in this.context.User_Specialty
                              where user.User.Id == userId
                              join special in this.context.Specialties on user.Specialty.Id equals special.Id
                              join univer in this.context.Universities on special.University.Id equals univer.Id
                              join d in this.context.Districts on univer.District.Id equals d.Id
                              orderby this.ratingProvider.GetRating(univer.Rating, special.NumApplication, special.NumEnrolled) descending
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
                                  IsFavorite = true
                              };
            return specialties.Skip(page * this.constValues.Value.CountForPage).Take(this.constValues.Value.CountForPage).ToList();
        }

        /// <summary>
        /// This method retrieves user's personal information
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>User personal information</returns>
        public UserPersonalInfo GetPersonalInfo(string userId)
        {
            var userInfo = (from user in this.context.Users
                            where user.Id == userId
                            join district in this.context.Districts on user.District.Id equals district.Id
                            select new UserPersonalInfo()
                            {
                                District = district.Name,
                                Email = user.Email,
                                FirstName = user.FirstName,
                                Surname = user.Surname,
                                Phone = user.PhoneNumber
                            }).First();

            return userInfo;
        }

        /// <summary>
        /// This method returns a list of test for which we have saved results
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns> Collection of tests </returns>
        public IEnumerable<Test> GetTestResults(string userId)
        {
            return (from tr in this.context.TestResult
                    where tr.User.Id == userId
                    join tests in this.context.Tests on tr.TestDetailedInfo.Id equals tests.Id
                    select new Test()
                    {
                        Id = tr.Id,
                        Name = tests.Name
                    }).ToList();
        }

        /// <summary>
        /// This method returns test results for a specific test and user
        /// </summary>
        /// <param name="testId">Selected test</param>
        /// <param name="userId">User Id</param>
        /// <returns> Test Results for a selected tests</returns>
        public IEnumerable<DirectionScores> GetTestResult(int testId, string userId)
        {
            var x = (from str in this.context.TestScore
                     join tr in this.context.TestResult on str.TestResult.Id equals tr.Id
                     join gd in this.context.GeneralDirections on str.GeneralDirection.Id equals gd.Id
                     where tr.Id == testId &&
                     tr.User.Id == userId
                     select new DirectionScores()
                     {
                         GeneralDir = new Common.DTO.GeneralDirection() { Id = gd.Id, Name = gd.Name },
                         Score = str.Score
                     }).ToList();

            if (x.Count < 1)
            {
                throw new ArgumentException(string.Format("No TestResults by Id of {0} were found", testId));
            }

            return x;
        }

        /// <summary>
        /// This method adds selected specialty to favorites
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="specialtyId">Specialty Id </param>
        /// <returns> Logical flag that represents operation status</returns>
        public bool AddSpecialtyToFavorite(string userId, int specialtyId)
        {
            User_Specialty add = new User_Specialty();
            add.Specialty = this.context.Specialties.Where(x => x.Id == specialtyId).First();
            add.User = this.context.Users.Where(x => x.Id == userId).First();
            var rez = this.context.User_Specialty.Where(x => x.Specialty.Id == specialtyId && x.User.Id == userId).FirstOrDefault();
            if (rez != null)
            {
                return false;
            }

            this.context.User_Specialty.Add(add);
            this.context.SaveChanges();
            return true;
        }

        /// <summary>
        /// This method removes specialty from favorites
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="specialtyId">Specialty Id</param>
        /// <returns> Logical flag that represents operation status</returns>
        public bool RemoveSpecialtyFromFavorite(string userId, int specialtyId)
        {
            User_Specialty remove = this.context.User_Specialty.First(x => x.Specialty.Id == specialtyId && x.User.Id == userId);
            this.context.User_Specialty.Remove(remove);
            this.context.SaveChanges();
            return true;
        }

        /// <summary>
        /// This method retrieves count of favorite specialties
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Count of favorite specialties</returns>
        public Count CountOfFavoriteSpecialtys(string userId)
        {
            Count result = new Count();
            result.AllElements = this.context.User_Specialty.Where(x => x.User.Id == userId).Count();
            result.ForOnePage = this.constValues.Value.CountForPage;
            return result;
        }
    }
}
