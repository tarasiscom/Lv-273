using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EPA.Common.Interfaces;
using EPA.Common.DTO;

namespace EPA.Web.Controllers
{
    [Route("api/Date")]
    public class DateController : Controller
    {
        private ILastSyncProvider lastSyncProvider;
       public DateController(ILastSyncProvider lastSyncProvider)
        {
            this.lastSyncProvider = lastSyncProvider;
        }
        // GET: api/Data
        [HttpGet("[action]")]
        public DateTime GetDate()
        {
            return lastSyncProvider.GetDate().DateValue;
           // return DateTime.Now;
        }
          
       
    }
}
