using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Calculations;
using Microsoft.Extensions.Options;

namespace EPA.MSSQL.SQLDataAccess
{
    public class SpecialtyProvider : ISpecialtyProvider
    {
        private readonly EpaContext context;
        private readonly IOptions<ConstSettings> constValues;

        public SpecialtyProvider(EpaContext cont, IOptions<ConstSettings> constSettings)
        {
            this.context = cont;
            this.constValues = constSettings;
        }

        public Common.DTO.Specialties GetSpecialtiesByDirection(DirectionInfo directionInfo)
        {
            Common.DTO.Specialties result = new Common.DTO.Specialties();

            var qry = from s in this.context.Specialties
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
            result.Count = qry.Count();
            result.List = qry.Skip(directionInfo.Page * directionInfo.CountOfElementsOnPage).Take(directionInfo.CountOfElementsOnPage);
            return result;
        }

        public Common.DTO.Specialties GetSpecialtiesByDirectionAndDistrict(DirectionAndDistrictInfo directionAndDistrictinfo)
        {
            Common.DTO.Specialties result;

            if (directionAndDistrictinfo.District == this.constValues.Value.AllDistricts)
            {
                result = this.GetSpecialtiesByDirection(directionAndDistrictinfo);
            }
            else
            {
                result = this.GetSpecialtiesByDirectionAndDistrictAll(directionAndDistrictinfo);
            }

            return result;
        }

        public Common.DTO.Specialties GetSpecialtiesByDirectionAndDistrictAll(DirectionAndDistrictInfo directionAndDistrict)
        {
            Common.DTO.Specialties result = new Common.DTO.Specialties();

            var qry = from s in this.context.Specialties
                      where s.Direction.GeneralDirection.Id == directionAndDistrict.GeneralDirection
                      join u in this.context.Universities on s.University.Id equals u.Id
                      join d in this.context.Districts on u.District.Id equals d.Id
                      where d.Id == directionAndDistrict.District
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
            result.Count = qry.Count();
            result.List = qry.Skip(directionAndDistrict.Page * directionAndDistrict.CountOfElementsOnPage).Take(directionAndDistrict.CountOfElementsOnPage);
            return result;
        }

        public IEnumerable<EPA.Common.DTO.GeneralDirection> GetGeneralDirections() => this.context.GeneralDirections.Select(x => x.ToCommon());

        public Common.DTO.Specialties GetSpecialtyBySubjects(ListSubjectsAndDistrict listSubjectsAndDistrict)
        {
            Common.DTO.Specialties result = new Specialties();

            // Check if District = All
            if (listSubjectsAndDistrict.District == this.constValues.Value.AllDistricts)
            {
                var listId = (from ss in this.context.Specialty_Subjects
                         group ss.Subject.Id by ss.Specialty.Id into grouped
                         where listSubjectsAndDistrict.ListSubjects.All(x => grouped.Contains(x)) &&
                         grouped.Count() >= listSubjectsAndDistrict.ListSubjects.Count()
                         select grouped.Key).ToList();
                result.Count = listId.Count;
                result.List = this.GetSpecialty(listSubjectsAndDistrict, listId);
            }
            else
            {
                var listId = (from ss in this.context.Specialty_Subjects
                         where ss.Specialty.University.District.Id == listSubjectsAndDistrict.District
                         group ss.Subject.Id by ss.Specialty.Id into grouped
                         where listSubjectsAndDistrict.ListSubjects.All(x => grouped.Contains(x)) &&
                         grouped.Count() >= listSubjectsAndDistrict.ListSubjects.Count()
                         select grouped.Key).ToList();
                result.Count = listId.Count;
                result.List = this.GetSpecialty(listSubjectsAndDistrict, listId);
            }

            return result;
        }

        public IEnumerable<Common.DTO.Subject> GetAllSubjects()
        {
            return this.context.Subjects.Select(x => x.ToCommon());
        }

        public IEnumerable<Common.DTO.District> GetAllDistricts()
        {
            return this.context.Districts.Select(x => x.ToCommon());
        }

        private IEnumerable<Common.DTO.Specialty> GetSpecialty(ListSubjectsAndDistrict subjects, List<int> listId)
        {
            return (from s in this.context.Specialties
                    join u in this.context.Universities on s.University.Id equals u.Id
                    where listId.Contains(s.Id)
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
                    }).Skip(subjects.page * subjects.countOfElementsOnPage).Take(subjects.countOfElementsOnPage).ToList();
        }
    }
}
