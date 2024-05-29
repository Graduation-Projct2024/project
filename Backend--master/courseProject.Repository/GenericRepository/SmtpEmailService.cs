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
//using MailKit;
//using MimeKit;
//using MailKit.Security;
//using MailKit.Net.Smtp;
//using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace courseProject.Repository.GenericRepository
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        private string fromEmail;
        private string fromEmailPassword;
        private string SmtpServer;
        private int port;
        private string UserName;
        public SmtpEmailService( IConfiguration configuration) 
        {

            fromEmail = configuration.GetSection("EmailConfiguration")["From"];
            fromEmailPassword = configuration.GetSection("EmailConfiguration")["Password"];
            SmtpServer = configuration.GetSection("EmailConfiguration")["SmtpServer"];
            port = int.Parse(configuration.GetSection("EmailConfiguration")["Port"]);
            UserName= configuration.GetSection("EmailConfiguration")["Username"];
        }
        

        public async Task SendEmailAsync(string to, string subject, string body)
        {



            var smtpClient = new SmtpClient(SmtpServer, port)
            {
                //Port =(port),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UserName, fromEmailPassword),
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


        //public async void SendEmail(string email, string subject, string messageBody)
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Course Academy", fromEmail));
        //    message.To.Add(new MailboxAddress("", email));
        //    message.Subject = subject;
        //    message.Body = new TextPart("plain") { Text = messageBody };

        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect(SmtpServer, port, SecureSocketOptions.StartTls);
        //        client.Authenticate(fromEmail, fromEmailPassword);
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}
    }
}
