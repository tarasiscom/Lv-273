using System.Collections.Generic;
using System.Linq;
using EPA.Common.DTO;
using EPA.Common.Interfaces;
using EPA.MSSQL.Models;
using Microsoft.Extensions.Options;
using EPA.MSSQL.BusLogic;
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
        //temp
        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirection(int idDirection)
        {
            return (from s in this.context.Specialties
                    join u in this.context.Universities on s.University.Id equals u.Id
                    join d in this.context.Districts on u.DistrictID equals d.Id
                    where s.Direction.GeneralDirection.Id == idDirection
                    select new Common.DTO.Specialty()
                    {
                        Name = s.Name,
                        Address = u.Address,
                        District = d.Name,
                        Site = u.Site,
                        Rating = CalculatingProvider.GetRating(s.NumApplication, s.NumEnrolled),
                        University = u.Name
                    }).Distinct().OrderByDescending(o => o.Rating);
        }

        public IEnumerable<EPA.Common.DTO.GeneralDirection> GetGeneralDirections() 
                                                            => this.context.GeneralDirections.Select(x => x.ToCommon());


        public IEnumerable<Common.DTO.Specialty> GetSpecialtyBySubjects(List<int> listOfSubjects)
        {

            /*return (from sp in this.context.Specialties
                    where sp. 
                        (from ss in this.context.Specialty_Subjects
                        where ss.Subject.Id in listOfSubjects.Select(x=>x.Id))*/
            return null;
                
        }

        public IEnumerable<Common.DTO.Subject> GetAllSubjects() => this.context.Subjects.Select(x => x.ToCommon());

        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirectionWithPagination(int idDirection, int page)
        {
            var serviceProvider = context.GetInfrastructure<IServiceProvider>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new MyLoggerProvider());
            int numberOfObjectsPerPage = 20;
            var qry = from s in this.context.Specialties
                       join u in this.context.Universities on s.University.Id equals u.Id
                       join d in this.context.Districts on u.DistrictID equals d.Id
                       where s.Direction.GeneralDirection.Id == idDirection
                       select new Common.DTO.Specialty()
                       {
                           Name = s.Name,
                           Address = u.Address,
                           District = d.Name,
                           Site = u.Site,
                           Rating = CalculatingProvider.GetRating(s.NumApplication, s.NumEnrolled),
                           University = u.Name
                       };
            return qry.OrderByDescending(o => o.Rating).Skip((page - 1) * numberOfObjectsPerPage).Take(numberOfObjectsPerPage);
        }

    }
}
