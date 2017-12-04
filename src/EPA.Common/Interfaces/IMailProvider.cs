using System.Net.Mail;

namespace EPA.Common.Interfaces
{
    /// <summary>
    /// Interface that contains methods for manipulations with mail
    /// </summary>
    public interface IMailProvider
    {
        /// <summary>
        /// Method that send mail for account confirmation
        /// </summary>
        /// <param name="toAddress">Recipient's address</param>
        /// <param name="confirmationLink">Link to confirm account</param>
        /// <param name="userName">User first name</param>
        void SendMail(MailAddress toAddress, string confirmationLink, string userName);
    }
}
