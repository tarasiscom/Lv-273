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

        public IEnumerable<Common.DTO.Specialty> GetSpecialtyBySubjects(ListSubjectsAndDistrict listSubjectsAndDistrict)
        {
            if (listSubjectsAndDistrict.District == 0)
            {
                var q = (from ss in this.context.Specialty_Subjects
                         group ss.Subject.Id by ss.Specialty.Id into grouped
                         where grouped.All(item => listSubjectsAndDistrict.ListSubject.Contains(item))&&
                         grouped.Count() >= listSubjectsAndDistrict.ListSubject.Count()
                         select grouped.Key).ToList();

                return this.GetSpecialty(listSubjectsAndDistrict, q);
            }
            else
            {
                var q = (from ss in this.context.Specialty_Subjects
                         where ss.Specialty.University.District.Id == listSubjectsAndDistrict.District
                         group ss.Subject.Id by ss.Specialty.Id into grouped
                         where grouped.All(item => listSubjectsAndDistrict.ListSubject.Contains(item))&&
                         grouped.Count() >= listSubjectsAndDistrict.ListSubject.Count()
                         select grouped.Key).ToList();

                return this.GetSpecialty(listSubjectsAndDistrict, q);
            }
        }

        public IEnumerable<Common.DTO.Subject> GetAllSubjects() => this.context.Subjects.Select(x => x.ToCommon());

        public IEnumerable<Common.DTO.District> GetAllDistricts() => this.context.Districts.Select(x => x.ToCommon());

        private IEnumerable<Common.DTO.Specialty> GetSpecialty(ListSubjectsAndDistrict subjects, List<int> q)
        {
            var sub = (from sb in this.context.Subjects
                       where subjects.ListSubject.Contains(sb.Id)
                       select new Common.DTO.Subject()
                       {
                           Id = sb.Id,
                           Name = sb.Name
                       }).ToList();

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
                        Subjects = sub
                    }).ToList();
        }
    }
}
