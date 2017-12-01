using System.Net.Mail;

namespace EPA.Common.Interfaces
{
    public interface IMailProvider
    {
        void SendMail(MailAddress toAddress, string confirmationLink);
    }
}
