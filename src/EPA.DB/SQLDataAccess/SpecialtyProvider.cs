using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Calculations;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace EPA.MSSQL.SQLDataAccess
{
    /// <summary>
    /// This class contains methods for obtaining specialties data from database
    /// </summary>
    public class SpecialtyProvider : ISpecialtyProvider
    {
        private readonly EpaContext context;
        private readonly IOptions<ConstSettings> constValues;
        private readonly RatingProvider ratingProvider;

        public SpecialtyProvider(EpaContext context, IOptions<ConstSettings> constValues)
        {
            this.context = context;
            this.constValues = constValues;
            ratingProvider = new RatingProvider(constValues.Value.KoefOfNumApplication);
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
        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirectionAndDistrict(int idDirection, int idDistrict, int page)
        {
            IEnumerable<Specialty> specialties;

            if (idDistrict == this.constValues.Value.AllDistricts)
            {
                specialties = this.GetSpecialtiesByDirection(idDirection, page);
            }
            else
            {
                specialties = this.GetSpecialtiesByDirectionAndDistrictAll(idDirection, idDistrict, page);
            }

            return specialties;
        }

        /// <summary>
        /// This method retrieves collection of specialties which relates to chosen direction
        /// </summary>
        /// <param name="directionInfo">Information about direction for which specialties are filtered</param>
        /// <returns>Collection of specialties with their amount</returns>
        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirection(int idDirection, int page)
        {
            var specialties = from s in this.context.Specialties
                              where s.Direction.GeneralDirection.Id == idDirection
                              join u in this.context.Universities on s.University.Id equals u.Id
                              join d in this.context.Districts on u.District.Id equals d.Id
                              orderby ratingProvider.GetRating(u.Rating, s.NumApplication, s.NumEnrolled) descending
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

            return specialties.Skip(page * constValues.Value.CountForPage).Take(constValues.Value.CountForPage);
        }

        /// <summary>
        /// This method retrieves collection of specialties which relates to chosen direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirectionAndDistrictAll(int idDirection, int idDistrict, int page)
        {
            IEnumerable<Specialty> result;
            var specialties = from s in this.context.Specialties
                              where s.Direction.GeneralDirection.Id == idDirection
                              join u in this.context.Universities on s.University.Id equals u.Id
                              join d in this.context.Districts on u.District.Id equals d.Id
                              where d.Id == idDistrict
                              orderby ratingProvider.GetRating(u.Rating, s.NumApplication, s.NumEnrolled) descending
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

            result = specialties.Skip(page * constValues.Value.CountForPage).Take(constValues.Value.CountForPage);
            return result;
        }

        /// <summary>
        /// This method retrieves collection of specialties that relates to chosen subjets and district
        /// </summary>
        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtyBySubjects(List<int> listSubjects, int idDistrict, int page)
        {
          
            if (idDistrict == this.constValues.Value.AllDistricts)
            {
                var listId = (from ss in this.context.Specialty_Subjects
                              group ss.Subject.Id by ss.Specialty.Id into grouped
                              where listSubjects.All(x => grouped.Contains(x)) &&
                              grouped.Count() >= listSubjects.Count()
                              select grouped.Key).ToList();

                return this.GetSpecialty(page, listId);
            }
            else
            {
                var listId = (from ss in this.context.Specialty_Subjects
                              where ss.Specialty.University.District.Id == idDistrict
                              group ss.Subject.Id by ss.Specialty.Id into grouped
                              where listSubjects.All(x => grouped.Contains(x)) &&
                              grouped.Count() >= listSubjects.Count()
                              select grouped.Key).ToList();
                return this.GetSpecialty(page, listId);
            }
        }

       
        public Count GetCountByDirection(int directionId, int districtId)
        {
            Count result = new Count();
            if (districtId == this.constValues.Value.AllDistricts)
            {
                var count = (from s in this.context.Specialties
                             where s.Direction.GeneralDirection.Id == directionId
                             join u in this.context.Universities on s.University.Id equals u.Id
                             join d in this.context.Districts on u.District.Id equals d.Id
                             select new Common.DTO.Specialty()).Count();


                result.AllElements = count;
            }
            else
            {
                var count = (from s in this.context.Specialties
                             where s.Direction.GeneralDirection.Id == directionId
                             join u in this.context.Universities on s.University.Id equals u.Id
                             join d in this.context.Districts on u.District.Id equals d.Id
                             where d.Id == districtId
                             select new Common.DTO.Specialty()).Count();


                result.AllElements = count;
            }

            result.ForOnePage = constValues.Value.CountForPage;
            return result;
        }

       public Count GetCountBySubjects(List<int> listSubjects, int idDistrict)
        {
            Count result = new Count();
            if (idDistrict == this.constValues.Value.AllDistricts)
            {
                var count = (from ss in this.context.Specialty_Subjects
                             group ss.Subject.Id by ss.Specialty.Id into grouped
                             where listSubjects.All(x => grouped.Contains(x)) &&
                             grouped.Count() >= listSubjects.Count()
                             select grouped.Key).Count();

                result.AllElements = count;
            }
            else
            {
                var count = (from ss in this.context.Specialty_Subjects
                             where ss.Specialty.University.District.Id == idDistrict
                             group ss.Subject.Id by ss.Specialty.Id into grouped
                             where listSubjects.All(x => grouped.Contains(x)) &&
                             grouped.Count() >= listSubjects.Count()
                             select grouped.Key).Count();

                result.AllElements = count;
            }

            result.ForOnePage = constValues.Value.CountForPage;
            return result;
        }

        private IEnumerable<Common.DTO.Specialty> GetSpecialty(int page, List<int> listId)
        {
            return (from s in this.context.Specialties
                    join u in this.context.Universities on s.University.Id equals u.Id
                    where listId.Contains(s.Id)
                    orderby ratingProvider.GetRating(u.Rating, s.NumApplication, s.NumEnrolled) descending
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
                    }).Skip(page * constValues.Value.CountForPage).Take(constValues.Value.CountForPage).ToList();
        }
    }
}

