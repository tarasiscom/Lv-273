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
            ProfTestResult profRes = new ProfTestResult();
            var res = context.Directions.Join(context.ProfDirections,
                d => d.Id, p => p.Id,
                (d, p) => new { Name = d.Name, MaxP = p.MaxPoint, MinP = p.MinPoint }).Where(p => points >= p.MinP && p.MaxP < points ).ToList();
            profRes.ProfDirection = res[0].Name;

            profRes.ProfSpecialties = new List<Common.dto.Specialty>();
            var resSp = context.Specialties.Join(context.Universities,
                s => s.Id, u => u.Id,
                (s, u) => new { spec = s.Name, univ = u.Name, distr = u.District, addr = u.Address, site = u.Site}).ToList();
             foreach(var sp in resSp)
            {
                Common.dto.Specialty tmp = new Common.dto.Specialty();
                tmp.SpecialtyName = sp.spec;
                tmp.University = sp.univ;
                tmp.District = sp.distr;
                tmp.Address = sp.addr;
                tmp.Site = sp.site;
                profRes.ProfSpecialties.Add(tmp);
            }

            return profRes;
        }
    }
}
