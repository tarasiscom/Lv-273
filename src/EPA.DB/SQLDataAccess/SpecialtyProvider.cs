using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Models;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
                        District = u.District,
                        Site = u.Site,
                        University = u.Name
                    }).Distinct();
        }

        public IEnumerable<EPA.Common.DTO.GeneralDirection> GetGeneralDirections() => this.context.GeneralDirections.Select(x => x.ToCommon());

        public IEnumerable<Common.DTO.Specialty> GetSpecialtyBySubjects(List<int> listOfSubjects)
        {
            var serviceProvider = this.context.GetInfrastructure<IServiceProvider>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new MyLoggerProvider());

            var q = (from ss in this.context.Specialty_Subjects
                     group ss.Subject.Id by ss.Specialty.Id into grouped
                     where grouped.All(item => listOfSubjects.Contains(item))
                     select grouped.Key )
                     .ToList();

            return (from s in this.context.Specialties
                    join u in this.context.Universities on s.University.Id equals u.Id
                    where q.Contains(s.Id)
                    select new Common.DTO.Specialty()
                    {
                        Name = s.Name,
                        Address = u.Address,
                         District = "область",
                        Site = u.Site,
                        University = u.Name
                    }).ToList();
        }

        public bool IsSubject(List<KeyId> q, int id)
        {
            bool king = false;
            foreach (var tmp in q)
            {
                if (tmp.Id == id)
                {
                    king = true;
                    break;
                }
            }

            return king;
        }

        public IEnumerable<Common.DTO.Subject> GetAllSubjects() => this.context.Subjects.Select(x => x.ToCommon());
    }

    public class KeyId
    {
        public int Id { get; set; }
    }
}
