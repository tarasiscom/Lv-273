using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<ConstSettings> constValues;

        public AccountController(UserManager<MSSQL.Models.User> userManager, SignInManager<MSSQL.Models.User> signInManager, IMailProvider mailProvider, IOptions<ConstSettings> constValues)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailProvider = mailProvider;
            this.constValues = constValues;
        }

        [Route("api/registration")]
        [HttpPost]
        [AllowAnonymous]
        public ObjectResult Register([FromBody]MSSQL.Models.User newUser)
        {
            var result = this.userManager.CreateAsync(newUser, newUser.PasswordHash).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                var confirmationToken = this.userManager.GenerateEmailConfirmationTokenAsync(newUser).GetAwaiter().GetResult();
                var confirmationLink = this.Url.Action(
                                    "confirmEmail",
                                    "account",
                                    new { userid = newUser.Id, token = confirmationToken },
                                    protocol: this.HttpContext.Request.Scheme);

                var toAddress = new MailAddress(newUser.Email);
                this.mailProvider.SendMail(toAddress, confirmationLink);
                return this.Ok(this.constValues.Value.RegistrSuccess);
            }
            else
            {
                string errorDecription = null;
                foreach (var error in result.Errors)
                {
                    errorDecription = error.Description;
                    break;
                }

                return this.BadRequest(errorDecription);
            }
        }

        [Route("account/confirmEmail")]
        [AllowAnonymous]
        public IActionResult ConfirmEmail([FromQuery]string userid, [FromQuery]string token)
        {
            MSSQL.Models.User user = this.userManager.FindByIdAsync(userid).GetAwaiter().GetResult();
            IdentityResult result = this.userManager.ConfirmEmailAsync(user, token).GetAwaiter().GetResult();
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
