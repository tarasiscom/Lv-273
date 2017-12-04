using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EPA.Common.DTO;
using System;

namespace EPA.Web.Controllers
{
    /// <summary>
    /// API for user creation and sign in
    /// </summary>
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
            this.signInManager = signInManager;
            this.constValues = constValues;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="newUser">Full information about new user</param>
        /// <returns>Returns status</returns>
        [Route("api/registration")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult  Register([FromBody]MSSQL.Models.User newUser)
        {
            EPA.MSSQL.Models.District district = new EPA.MSSQL.Models.District();
            district.Id = 0;
            newUser.District = district;
            var result = this.userManager.CreateAsync(newUser, newUser.PasswordHash).GetAwaiter().GetResult();
            Status status = new Status();

            if (result.Succeeded)
            {
                var confirmationToken = this.userManager.GenerateEmailConfirmationTokenAsync(newUser).GetAwaiter().GetResult();
                var confirmationLink = this.Url.Action(
                                    "confirmEmail",
                                    "account",
                                    new { userid = newUser.Id, token = confirmationToken },
                                    protocol: this.HttpContext.Request.Scheme);

                var toAddress = new MailAddress(newUser.Email);
                this.mailProvider.SendMail(toAddress, confirmationLink, newUser.FirstName);
                return this.Ok(new { Message = this.constValues.Value.RegistrSuccess});
            }
            else
            {
                string errorDecription = null;
                foreach (var error in result.Errors)
                {
                    errorDecription = error.Description;
                    break;
                }

                return this.BadRequest( new {Message = errorDecription });
            }
        }

        /// <summary>
        /// Confirm account when user click on link in email
        /// </summary>
        /// <param name="userid">User identifier</param>
        /// <param name="token">Token</param>
        /// <returns>Returns status</returns>
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

        [Route("api/CheckAuth")]
        [HttpGet]
        [AllowAnonymous]
        public bool CheckAuth()
        {
            return this.User.Identity.IsAuthenticated;
        }

        [Route("account/Logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return this.Redirect("/");
        }

        [Route("api/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginUser([FromBody]EPA.Common.DTO.LoginUser loginUser)
        {
            MSSQL.Models.User signedUser = userManager.FindByEmailAsync(loginUser.Email).GetAwaiter().GetResult();
            var result = signInManager.PasswordSignInAsync(signedUser.UserName, loginUser.Password, isPersistent:true, lockoutOnFailure:false).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                return this.Ok(new { Message = this.constValues.Value.RegistrSuccess });
            }
            return this.BadRequest();
        }
    }
}
