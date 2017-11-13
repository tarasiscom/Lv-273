using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Models;

namespace EPA.MSSQL.SQLDataAccess
{
    public class SpecialtyProvider : ISpecialtyProvider
    {
        private readonly EpaContext context;

        public SpecialtyProvider(EpaContext cont)
        {
            this.context = cont;
        }

        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirection(int idDirection)
        {
            return (from s in this.context.Specialties
                    join u in this.context.Universities on s.University.Id equals u.Id
                    where s.Direction.GeneralDirection.Id == idDirection
                    select new Common.DTO.Specialty()
                    {
                        Name = s.Name,
                        Address = u.Address,
                        District = u.District.Name,
                        Site = u.Site,
                        University = u.Name
                    }).Distinct();
        }

        public IEnumerable<EPA.Common.DTO.GeneralDirection> GetGeneralDirections() => this.context.GeneralDirections.Select(x => x.ToCommon());

        public Common.DTO.SpecialtiesAndCount GetSpecialtyBySubjects(ListSubjectsAndDistrict listSubjectsAndDistrict)
        {
            if (listSubjectsAndDistrict.District == 0)
            {
                var q = (from ss in this.context.Specialty_Subjects
                         group ss.Subject.Id by ss.Specialty.Id into grouped
                         where listSubjectsAndDistrict.ListSubjects.All(x => grouped.Contains(x)) &&
                         grouped.Count() >= listSubjectsAndDistrict.ListSubjects.Count()
                         select grouped.Key).ToList();

                Common.DTO.SpecialtiesAndCount result = new SpecialtiesAndCount();
                result.ListSpecialties = this.GetSpecialty(listSubjectsAndDistrict, q);
                result.CountOfAllElements = q.Count;
                return result;
            }
            else
            {
                var q = (from ss in this.context.Specialty_Subjects
                         where ss.Specialty.University.District.Id == listSubjectsAndDistrict.District
                         group ss.Subject.Id by ss.Specialty.Id into grouped
                         where listSubjectsAndDistrict.ListSubjects.All(x => grouped.Contains(x)) &&
                         grouped.Count() >= listSubjectsAndDistrict.ListSubjects.Count()
                         select grouped.Key).ToList();


                Common.DTO.SpecialtiesAndCount result = new SpecialtiesAndCount();
                result.ListSpecialties = this.GetSpecialty(listSubjectsAndDistrict, q);
                result.CountOfAllElements = q.Count;
                return result;
            }
        }

        public IEnumerable<Common.DTO.Subject> GetAllSubjects() => this.context.Subjects.Select(x => x.ToCommon());

        public IEnumerable<Common.DTO.District> GetAllDistricts() => this.context.Districts.Select(x => x.ToCommon());

        private IEnumerable<Common.DTO.Specialty> GetSpecialty(ListSubjectsAndDistrict subjects, List<int> q)
        {
            return (from s in this.context.Specialties
                    join u in this.context.Universities on s.University.Id equals u.Id
                    where q.Contains(s.Id)
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
                    }).Skip((subjects.page - 1) * subjects.countOfElementsOnPage).Take(subjects.countOfElementsOnPage).ToList();
        }
    }
}
