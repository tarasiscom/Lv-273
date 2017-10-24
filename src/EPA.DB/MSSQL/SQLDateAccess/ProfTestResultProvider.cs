using System;
using System.Collections.Generic;
using System.Text;
using EPA.Common.Interfaces;
using EPA.Common.dto;
using EPA.DB.MSSQL.Models;
using System.Linq;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class ProfTestResultProvider : IProfTestResultProvider
    {
        EpaContext context;
        public ProfTestResultProvider()
        {
            this.context = new EpaContext();
        }

        public ProfTestResult GetUserResult(int points, int testId)
        {
            return new ProfTestResult()
            {
                ProfDirection = (from d in context.Directions
                                 join pd in context.ProfDirections on d.Id equals pd.Id
                                 where points >= pd.MinPoint && pd.MaxPoint < points
                                 select d.Name).FirstOrDefault(),
                ProfSpecialties = (from s in context.Specialties
                                   join u in context.Universities on s.Id equals u.Id
                                   select new Common.dto.Specialty()
                                   {
                                       SpecialtyName = s.Name,
                                       Address = u.Address,
                                       District = u.District,
                                       Site = u.Site,
                                       University = u.Name
                                   }).Take(5).ToList()
            };
        }
    }
}
