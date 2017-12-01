using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EPA.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MSSQL.Models.User> userManager;
        private readonly SignInManager<MSSQL.Models.User> signInManager;
        private readonly IMailProvider mailProvider;

        public AccountController(UserManager<MSSQL.Models.User> userManager, SignInManager<MSSQL.Models.User> signInManager, IMailProvider mailProvider)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailProvider = mailProvider;
        }

        [Route("api/registration")]
        [HttpPost]
        [AllowAnonymous]
        public async Task RegisterAsync([FromBody]MSSQL.Models.User newUser)
        {
            var result = await this.userManager.CreateAsync(newUser, newUser.PasswordHash);
            if (result.Succeeded)
            {
                var confirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = this.Url.Action(
                                    "confirmEmail",
                                    "account",
                                    new { userid = newUser.Id, token = confirmationToken },
                                    protocol: this.HttpContext.Request.Scheme);

                var toAddress = new MailAddress(newUser.Email);
                this.mailProvider.SendMail(toAddress, confirmationLink);
            }
        }

        [Route("account/confirmEmail")]
        [AllowAnonymous]
        public IActionResult ConfirmEmail([FromQuery]string userid, [FromQuery]string token)
        {
            MSSQL.Models.User user = this.userManager.FindByIdAsync(userid).Result;
            IdentityResult result = this.userManager.ConfirmEmailAsync(user, token).Result;
            if (result.Succeeded)
            {
                return this.Redirect("/Login");
            }
            else
            {
                throw new ArgumentException("Invalid token");
            }
        }

        //[ValidateAntiForgeryToken]
        [Route("api/login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> LoginUser([FromBody]EPA.Common.DTO.LoginUser loginUser)
        {
            MSSQL.Models.User signedUser = await userManager.FindByEmailAsync(loginUser.Email);
            var result = await signInManager.PasswordSignInAsync(signedUser.UserName, loginUser.Password, true, false);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
