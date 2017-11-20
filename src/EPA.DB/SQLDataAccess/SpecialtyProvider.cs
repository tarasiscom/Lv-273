using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Calculations;
using Microsoft.Extensions.Options;

namespace EPA.MSSQL.SQLDataAccess
{
    /// <summary>
    /// This class contains methods for obtaining specialties data from database
    /// </summary>
    public class SpecialtyProvider : ISpecialtyProvider
    {
        private readonly EpaContext context;
        private readonly IOptions<ConstSettings> constValues;

        public SpecialtyProvider(EpaContext context, IOptions<ConstSettings> constSettings)
        {
            this.context = context;
            this.constValues = constSettings;
        }

        /// <summary>
        /// This method retrieves collection of all subjects from database
        /// </summary>
        /// <returns>Collection of subjects</returns>
        public IEnumerable<Common.DTO.Subject> GetAllSubjects() => this.context.Subjects.Select(x => x.ToCommon());

        /// <summary>
        /// This method retrieves collection of all districts from database
        /// </summary>
        /// <returns>Collection of districts</returns>
        public IEnumerable<Common.DTO.District> GetAllDistricts() => this.context.Districts.Select(x => x.ToCommon());

        /// <summary>
        /// This method retrieves collection of all general directions
        /// </summary>
        /// <returns>Collection of general directions</returns>
        public IEnumerable<EPA.Common.DTO.GeneralDirection> GetGeneralDirections() => this.context.GeneralDirections.Select(x => x.ToCommon());



        /// <summary>
        /// This method retrieves collection of specialties which relates to chosen direction and district
        /// </summary>
        /// <param name="directionInfo">Information about direction for which specialties are filtered</param>
        /// <returns></returns>
        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirectionAndDistrict(DirectionInfo directionInfo)
        {
            IEnumerable<Specialty> specialties;

            if (directionInfo.District == this.constValues.Value.AllDistricts)
            {
                specialties = this.GetSpecialtiesByDirection(directionInfo);
            }
            else
            {
                specialties = this.GetSpecialtiesByDirectionAndDistrictAll(directionInfo);
            }

            return specialties;
        }

        /// <summary>
        /// This method retrieves collection of specialties which relates to chosen direction
        /// </summary>
        /// <param name="directionInfo">Information about direction for which specialties are filtered</param>
        /// <returns>Collection of specialties with their amount</returns>
        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirection(DirectionInfo directionInfo)
        {
            var specialties = from s in this.context.Specialties
                              where s.Direction.GeneralDirection.Id == directionInfo.GeneralDirection
                              join u in this.context.Universities on s.University.Id equals u.Id
                              join d in this.context.Districts on u.District.Id equals d.Id
                              orderby RatingProvider.GetRating(s.NumApplication, s.NumEnrolled) descending
                              select new Common.DTO.Specialty()
                              {
                                  Name = s.Name,
                                  Address = u.Address,
                                  District = d.Name,
                                  Site = u.Site,
                                  University = u.Name,
                                  Subjects = (from ss in this.context.Specialty_Subjects
                                              where ss.Specialty.Id == s.Id
                                              select ss.Subject.ToCommon()).ToList()
                              };

            return specialties.Skip(directionInfo.Page * constValues.Value.CountForPage).Take(constValues.Value.CountForPage);
        }

        /// <summary>
        /// This method retrieves collection of specialties which relates to chosen direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirectionAndDistrictAll(DirectionInfo direction)
        {
            IEnumerable<Specialty> result;

            var specialties = from s in this.context.Specialties
                              where s.Direction.GeneralDirection.Id == direction.GeneralDirection
                              join u in this.context.Universities on s.University.Id equals u.Id
                              join d in this.context.Districts on u.District.Id equals d.Id
                              where d.Id == direction.District
                              orderby RatingProvider.GetRating(s.NumApplication, s.NumEnrolled) descending
                              select new Common.DTO.Specialty()
                              {
                                  Name = s.Name,
                                  Address = u.Address,
                                  District = d.Name,
                                  Site = u.Site,
                                  University = u.Name,
                                  Subjects = (from ss in this.context.Specialty_Subjects
                                              where ss.Specialty.Id == s.Id
                                              select ss.Subject.ToCommon()).ToList()
                              };

            result = specialties.Skip(direction.Page * constValues.Value.CountForPage).Take(constValues.Value.CountForPage);
            return result;
        }

        /// <summary>
        /// This method retrieves collection of specialties that relates to chosen subjets and district
        /// </summary>
        /// <param name="subjectsInfo"></param>
        /// <returns></returns>
        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtyBySubjects(SubjectsInfo subjectsInfo)
        {
            return (from s in this.context.Specialties
                    join u in this.context.Universities on s.University.Id equals u.Id
                    where subjectsInfo.ListSubjects.Contains(s.Id)
                    orderby RatingProvider.GetRating(s.NumApplication, s.NumEnrolled) descending
                    select new Common.DTO.Specialty()
                    {
                        Name = s.Name,
                        Address = u.Address,
                        District = u.District.Name,
                        Site = u.Site,
                        University = u.Name,
                        Subjects = (from ss in this.context.Specialty_Subjects
                                    where ss.Specialty.Id == s.Id
                                    select ss.Subject.ToCommon()).ToList()
                    }).Skip(subjectsInfo.Page * constValues.Value.CountForPage).Take(constValues.Value.CountForPage).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directioninfo"></param>
        /// <returns></returns>
        public Count GetCountByDirection(DirectionInfo directioninfo)
        {
            Count result = new Count();
            if (directioninfo.District == this.constValues.Value.AllDistricts)
            {
                var count = (from s in this.context.Specialties
                                   where s.Direction.GeneralDirection.Id == directioninfo.GeneralDirection
                                   join u in this.context.Universities on s.University.Id equals u.Id
                                   join d in this.context.Districts on u.District.Id equals d.Id
                                   where d.Id == directioninfo.District
                                   select new Common.DTO.Specialty()).Count();


                result.AllElements = count;
            }
            else
            {
                var count = (from s in this.context.Specialties
                                   where s.Direction.GeneralDirection.Id == directioninfo.GeneralDirection
                                   join u in this.context.Universities on s.University.Id equals u.Id
                                   join d in this.context.Districts on u.District.Id equals d.Id
                                   where d.Id == directioninfo.District
                                   select new Common.DTO.Specialty()).Count();


                result.AllElements = count;
            }

            result.ForOnePage = constValues.Value.CountForPage;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subjectsInfo"></param>
        /// <returns></returns>
        public Count GetCountBySubjects(SubjectsInfo subjectsInfo)
        {
            Count result = new Count();
            if (subjectsInfo.District == this.constValues.Value.AllDistricts)
            {
                var count = (from ss in this.context.Specialty_Subjects
                             group ss.Subject.Id by ss.Specialty.Id into grouped
                             where subjectsInfo.ListSubjects.All(x => grouped.Contains(x)) &&
                             grouped.Count() >= subjectsInfo.ListSubjects.Count()
                             select grouped.Key).Count();

                result.AllElements = count;
            }
            else
            {
                var count = (from ss in this.context.Specialty_Subjects
                             where ss.Specialty.University.District.Id == subjectsInfo.District
                             group ss.Subject.Id by ss.Specialty.Id into grouped
                             where subjectsInfo.ListSubjects.All(x => grouped.Contains(x)) &&
                             grouped.Count() >= subjectsInfo.ListSubjects.Count()
                             select grouped.Key).Count();

                result.AllElements = count;
            }

            result.ForOnePage = constValues.Value.CountForPage;
            return result;
        }
    }
}

