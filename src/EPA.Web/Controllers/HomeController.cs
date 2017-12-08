using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EPA.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// This method opens react app
        /// </summary>
        /// <returns>App View</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// This method opens error page / not used for now though, due to react error handling
        /// </summary>
        /// <returns>App View</returns>
        public IActionResult Error()
        {
            this.ViewData["RequestId"] = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
            return this.View();
        }
    }
}
