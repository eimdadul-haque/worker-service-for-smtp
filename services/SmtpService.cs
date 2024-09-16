using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using worker_service_for_smtp.Interfaces;
using worker_service_for_smtp.Models;

namespace worker_service_for_smtp.services
{
    public class SmtpService : ISmtpService
    {
        private readonly IConfiguration _configuration;
        public SmtpService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            using (var smtpClient = new SmtpClient(smtpSettings.Host, smtpSettings.Port))
            {
                smtpClient.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);
                smtpClient.EnableSsl = smtpSettings.EnableSsl;

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(smtpSettings.From),
                    Body = body,
                    Subject = subject,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent successfully.");
                }catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
