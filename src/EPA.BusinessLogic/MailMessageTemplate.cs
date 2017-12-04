namespace EPA.BusinessLogic
{
    public class MailMessageTemplate
    {
        public string GetConfirmMailMessage(string link, string userName)
        {
            string messageBody = "<p>" +
                "<img src = \"http://downloadicons.net/sites/default/files/graduation-icon-66502.png\" width=\"120\" height=\"120\" align=\"left\">" +
                "<br/>" +
                "<br/>" +
                "<h1>Educational Program Advisor</h1>" +
                "<br/>" +
                "<br/>" +
                "</p>" +
                "<br/> " +
                "<p> Привіт " + userName + ", <br />" +
                "Дякуємо за створення облікового запису EPA. " +
                "Для продовження, підтвердіть реєстрації перейшовши за посиланням. </p>" +
                "<br/>" +
                "<br/>" +
                "<a href= " + link + " class=\"btn btn-primary\" role=\"button\">" +
                "<button style=\"border: none; color: white; padding: 14px 28px; background-color: #4CAF50; \">" +
                "Підтвердити реєстрацію<button>" +
                "</a>" +
                "<br/><br/>" +
                "<p>З повагою, <br /> Команда EPA</p>";

            return messageBody;
        }
    }
}
