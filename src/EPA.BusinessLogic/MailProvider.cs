using EPA.Common.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EPA.BusinessLogic
{
    public class MailProvider : IMailProvider
    {
        private readonly IOptions<ConstSettings> constValues;

        public MailProvider(IOptions<ConstSettings> constValues)
        {
            this.constValues = constValues;
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
    }
}
