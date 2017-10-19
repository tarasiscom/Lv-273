using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPA.DB.MSSQL.Models;

namespace EPA.Web.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public TestContext dc = new TestContext();
        
        [HttpGet("[action]")]
        public IEnumerable<DateAPI> GetDates()
        {
            Random rng = new Random();
            List<DateAPI> temp = new List<DateAPI>();
            
            foreach (var v in dc.Dates.ToList())
            {
                temp.Add(new DateAPI { ID = Convert.ToInt32(v.Id), DT = v.DateValue.ToShortDateString() });
            }
            

            return temp.AsEnumerable();


        }

        public class DateAPI
        {
            public int ID { get; set; }
            public string DT { get; set; }


        }
    }
}
