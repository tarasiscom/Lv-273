using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using EPA.MSSQL.Models;

namespace EPA.Web.Controllers
{
    //[Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILoggerFactory loggerFactory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = loggerFactory.CreateLogger<AccountController>();
        }

        //[ValidateAntiForgeryToken]
        [Route("api/login")]
        [HttpPost]
        public async Task<string> LoginUser([FromBody]LoginUser loginUser)
        {
            User signedUser = await userManager.FindByEmailAsync(loginUser.Email);
            var result = await signInManager.PasswordSignInAsync(signedUser.UserName, loginUser.Password, false, false);
            if (result.Succeeded)
            {
                logger.LogInformation(1, "User logged in.");
                return "ok";
            }
            if (result.IsNotAllowed)
            {
                return "not allowed";
            }
            if (result.RequiresTwoFactor)
            {
                return "RequiresTwoFactor";
            }
            if (result.IsLockedOut)
            {
                return "IsLockedOut";
            }

            return "oops";
        }
    }
}