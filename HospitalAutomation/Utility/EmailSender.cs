using Microsoft.AspNetCore.Identity.UI.Services;

namespace HospitalAutomation.Utility
{
    public class EmailSender:IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Email işlemleri burada yapılabilir
            return Task.CompletedTask;
        }
    }
}
