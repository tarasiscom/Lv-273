using System.Collections.Generic;
using EPA.Common.Interfaces.ProfTest;
using EPA.Common.DTO.ProfTest.Quiz;
using EPA.DB.MSSQL.Models;
using System.Linq;

namespace EPA.DB.MSSQL.SQLDateAccess
{
    public class ProfTestResultProvider : ITestResultProvider
    {
        private EpaContext context;
        private const int NumberOfUniversities = 5;

        public ProfTestResultProvider()
        {
            this.context = new EpaContext();
        }

        public Result GetResult(int points, int testId)
        {
            string direction = (from d in this.context.Directions
                                join pd in this.context.ProfDirections on d.Id equals pd.Direction.Id
                                where points >= pd.MinPoint && pd.MaxPoint > points
                                select d.Name).FirstOrDefault();
            return new Result()
            {
                ProfDirection = direction,
                Specialties = (from s in this.context.Specialties
                                   join u in this.context.Universities on s.University.Id equals u.Id
                                   where s.Direction.Name == direction
                                   select new Common.DTO.Specialty()
                                   {
                                       Name = s.Name,
                                       Address = u.Address,
                                       District = u.District,
                                       Site = u.Site,
                                       University = u.Name
                                   }).Take(NumberOfUniversities).ToList()
            };
        }
    }
}
