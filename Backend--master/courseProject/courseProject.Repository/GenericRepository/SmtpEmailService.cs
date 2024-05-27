using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using courseProject.Core.IGenericRepository;
using courseProject.Repository.Data;
using Microsoft.Extensions.Configuration;

namespace courseProject.Repository.GenericRepository
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        private string fromEmail;
        private string fromEmailPassword;
        private string SmtpServer;
        private string port;
        public SmtpEmailService( IConfiguration configuration) 
        {

            fromEmail = configuration.GetSection("EmailConfiguration")["From"];
            fromEmailPassword = configuration.GetSection("EmailConfiguration")["Password"];
            SmtpServer = configuration.GetSection("EmailConfiguration")["SmtpServer"];
            port =  (configuration.GetSection("EmailConfiguration")["Port"]);
        }
        

        public async Task SendEmailAsync(string to, string subject, string body)
        {
          
        

            var smtpClient = new SmtpClient(SmtpServer)
            {
                Port =int.Parse( port),
                Credentials = new NetworkCredential(fromEmail, fromEmailPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, "Course Academy"  ),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
               
            };
            mailMessage.To.Add(to);

            try
            {
                await smtpClient.SendMailAsync(mailMessage );
            }
            catch (Exception ex)
            {               
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
