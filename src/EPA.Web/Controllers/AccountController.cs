using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Security.Claims;
using System;

namespace EPA.Web.Controllers
{
    /// <summary>
    /// API for user registration and login related operations
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
        /// This method provides registration of a new user
        /// </summary>
        /// <param name="newUser">Full information about new user</param>
        /// <returns>Returns status</returns>
        [Route("api/registration")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody]MSSQL.Models.User newUser)
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
                this.mailProvider.SendMail(toAddress, confirmationLink, newUser.FirstName);
                return this.Ok(new { Message = this.constValues.Value.RegistrSuccess });
            }
            else
            {
                string errorDecription = null;
                foreach (var error in result.Errors)
                {
                    errorDecription = error.Description;
                    break;
                }

                return this.BadRequest(new { Message = errorDecription });
            }
        }

        /// <summary>
        /// This method redirects to personal cabinet when user clicks confim email link.
        /// </summary>
        /// <param name="userid">User identIdifier</param>
        /// <param name="token">Authorization token</param>
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

        /// <summary>
        /// This method checks if user is currently authorized.
        /// </summary>
        /// <returns>Logical flag that checks is user authenticated</returns>
        [Route("api/CheckAuth")]
        [HttpGet]
        [AllowAnonymous]
        public bool CheckAuth()
        {
            return this.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// This method logouts user.
        /// </summary>
        /// <returns>Redirects to Home Page</returns>
        [Route("account/Logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            this.signInManager.SignOutAsync();
            return this.Redirect("/");
        }

        /// <summary>
        /// This method provides login funcionality for user
        /// </summary>
        /// <param name="loginUser">login information about user</param>
        /// <returns>Returns status</returns>
        [Route("api/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginUser([FromBody]EPA.Common.DTO.LoginUser loginUser)
        {
            MSSQL.Models.User signedUser = this.userManager.FindByEmailAsync(loginUser.Email).GetAwaiter().GetResult();
            if (signedUser != null)
            {
                var result = this.signInManager.PasswordSignInAsync(
                    signedUser.UserName,
                    loginUser.Password,
                    isPersistent: true,
                    lockoutOnFailure: false).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    return this.Ok(new { Message = "Авторизація пройшла успішно" });
                }
            }

            return this.BadRequest(new { Message = "Неправильно введений логін або пароль" });
        }

        /// <summary>
        /// This method changes user's password.
        /// </summary>
        /// <param name="passwords">Password information</param>
        /// <returns>Operation status string</returns>
        [Authorize]
        [HttpPost]
        [Route("api/User/ChangePassword")]
        public string ChangePassword([FromBody]EPA.Common.DTO.ChangePassword passwords)
        {
            string resultMessage;
            var user = this.userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            var changePasswordStatus = this.userManager
                .ChangePasswordAsync(user, passwords.OldPassword, passwords.NewPassword)
                .GetAwaiter().GetResult();

            if (changePasswordStatus.Succeeded)
            {
                resultMessage = "Ви успішно змінили пароль.";
            }
            else
            {
                resultMessage = "Пароль не вдалось змінити.";
            }

            return resultMessage;
        }

        /// <summary>
        /// This method gives user Id. / Dublicate of UserController method
        /// </summary>
        /// <param name="principal">Claim Principal</param>
        /// <returns>User ID</returns>
        public string GetUserId(ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
