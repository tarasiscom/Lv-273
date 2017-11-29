using EPA.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
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
                var confirmationLink = this.Url.RouteUrl(
                                    "ConfirmEmail",
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

        [Route("ConfirmEmail/{userid}/{token}", Name = "ConfirmEmail")]
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
