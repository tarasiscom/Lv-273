using EPA.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EPA.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MSSQL.Models.User> userManager;
        private readonly SignInManager<MSSQL.Models.User> signInManager;

        public AccountController(UserManager<MSSQL.Models.User> userManager, SignInManager<MSSQL.Models.User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Route("api/registration")]
        [HttpPost]
        [AllowAnonymous]
        public async Task RegisterAsync([FromBody]MSSQL.Models.User newUser)
        {
            var result = await this.userManager.CreateAsync(newUser);
            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(newUser, isPersistent: false);
            }

            // else
            // {
            // }
        }
    }
}
