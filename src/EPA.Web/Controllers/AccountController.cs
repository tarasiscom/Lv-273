using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
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
                // email confirm
                var confirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string confirmationLink = this.Url.Action(
                                                    "ConfirmEmail",
                                                    "AccountController",
                                                    new { userid = newUser.Id, token = confirmationToken },
                                                    protocol: this.HttpContext.Request.Scheme);

                var fromAddress = new MailAddress("epadvisor17@gmail.com");
                var fromPassword = "epadvisor2017";
                var toAddress = new MailAddress(newUser.Email);

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

            // else
            // {
            // }
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
    }
}
