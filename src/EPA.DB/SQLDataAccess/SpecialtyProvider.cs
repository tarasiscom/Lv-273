using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPA.MSSQL.Models;
using EPA.Common.Interfaces;
using EPA.MSSQL.BusLogic;

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

        public IEnumerable<EPA.Common.DTO.Specialty> GetSpecialtiesByDirectionWithPagination(int idDirection, int page)
        {
            int numberOfObjectsPerPage = 20;
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
                    }).Distinct().OrderByDescending(o => o.Rating).Skip((page - 1) * numberOfObjectsPerPage).Take(numberOfObjectsPerPage);
        }
    }
}
