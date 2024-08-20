using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;
        public EmailSender()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("danismazismail@gmail.com", "sexa qcvu hfwm cvty"),
                EnableSsl = true
            };
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("danismazismail@gmail.com"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Mail gönderilemedi!");
            }
        }
    }
}
