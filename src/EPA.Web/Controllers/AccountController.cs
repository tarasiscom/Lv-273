using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPA.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MSSQL.Models.User> userManager;
        private readonly SignInManager<MSSQL.Models.User> signInManager;
        private readonly IOptions<ConstSettings> constValues;

        public AccountController(UserManager<MSSQL.Models.User> userManager, SignInManager<MSSQL.Models.User> signInManager, IOptions<ConstSettings> constValues)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.constValues = constValues;
        }

        [Route("api/registration")]
        [HttpPost]
        [AllowAnonymous]
        public async Task RegisterAsync([FromBody]MSSQL.Models.User newUser)
        {
            var result = await this.userManager.CreateAsync(newUser, newUser.PasswordHash);
            if (result.Succeeded)
            {
                // email confirm
                var confirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string confirmationLink = this.Url.Action(
                                                    "ConfirmEmail",
                                                    "AccountController",
                                                    new { userid = newUser.Id, token = confirmationToken },
                                                    protocol: this.HttpContext.Request.Scheme);

                var toAddress = new MailAddress(newUser.Email);
                this.SendMail(toAddress, confirmationLink);
            }
        }

        public void SendMail(MailAddress toAddress, string confirmationLink)
        {
            var fromAddress = new MailAddress(this.constValues.Value.Email);
            var fromPassword = this.constValues.Value.EmailPassword;

            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Account confirm",
                Body = "Для підтвердження перейдіть за посиланням: " + confirmationLink
            };

            client.Send(message);
        }

        [HttpGet]
        [AllowAnonymous]
        public void ConfirmEmail(string userid, string token)
        {
            MSSQL.Models.User user = this.userManager.FindByIdAsync(userid).Result;
            IdentityResult result = this.userManager.ConfirmEmailAsync(user, token).Result;
            if (result.Succeeded)
            {
                this.ViewBag.Message = "Email confirmed successfully!";
            }
            else
            {
                this.ViewBag.Message = "Error while confirming your email!";
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
        public StatusCodeResult LoginUser([FromBody]EPA.Common.DTO.LoginUser loginUser)
        {
            MSSQL.Models.User signedUser = userManager.FindByEmailAsync(loginUser.Email).GetAwaiter().GetResult();
            if (signedUser != null)
            {
                var result = signInManager.PasswordSignInAsync(signedUser.UserName, loginUser.Password, isPersistent:true, lockoutOnFailure:false).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    return this.Ok();
                }
            }
            return this.BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("api/User/ChangePassword")]
        public string ChangePassword([FromBody]EPA.Common.DTO.ChangePassword passwords)
        {
            var user = userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            var a = userManager.ChangePasswordAsync(user, passwords.OldPassword, passwords.NewPassword).GetAwaiter().GetResult();
            var b = userManager.UpdateAsync(user);
            
            return a.Succeeded.ToString();
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

    }
}
