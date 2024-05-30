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
using MimeKit;
using Microsoft.Extensions.Options;
//using MailKit;
//using MimeKit;
//using MailKit.Security;
//using MailKit.Net.Smtp;
//using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace courseProject.Repository.GenericRepository
{
    public class SmtpEmailService
    { }
}
//    {
//        private readonly EmailConfiguration _emailConfig;

//        public SmtpEmailService(IOptions<EmailConfiguration> emailConfig)
//        {
//            _emailConfig = emailConfig.Value;
//        }

//        public SmtpEmailService(IConfiguration configuration)
//        {

//            this.configuration = configuration;
//            var emailConfig = configuration.GetSection("EmailConfiguration");
//            var smtpServer = emailConfig["SmtpServer"];
//            var port = int.Parse(emailConfig["Port"]);
//            var fromEmail = emailConfig["From"];
//            var fromEmailPassword = emailConfig["Password"];
//        }

//        //private const string SmtpServer = "smtp.gmail.com";
//        //private const int SmtpPort = 587;
//        //private const string SenderEmail = "courseacademytester@gmail.com";
//        //private const string SenderPassword = "lzpmfdcqsrmliaiq";

//        public async Task SendVerificationEmail(string ToEmail, string verificationCode)
//        {
//            using (var client = new SmtpClient(SmtpServer, SmtpPort))
//            {
//                client.EnableSsl = true;
//                client.Credentials = new NetworkCredential(SenderEmail, SenderPassword);

//                var message = new MailMessage(SenderEmail, ToEmail)
//                {
//                    Subject = "Account Verification",
//                    IsBodyHtml = true,
//                Body = $@"
//                    <html>
//                    <body>
//                        <div style='text-align: center;'>
//                            <img src='https://www.yourwebsite.com/logo.png' alt='Your Company Logo' style='width: 200px; height: auto;' />
//                            <h2>Dear User,</h2>
//                            <p>Thank you for joining our site.</p>
//                            <p>Here is your code to confirm your email:</p>
//                            <h1 style='color: #3498db;'>{verificationCode}</h1>
//                            <p>Best regards,</p>
                         
//                        </div>
//                    </body>
//                    </html>"
//                };

//                try
//                {
//                   await client.SendMailAsync(message);
//                }
//                catch (Exception ex)
//                {
//                    // Handle error
//                    throw ex;
//                }
//            }
//        }


//        //private MimeMessage CreateMimeMessage(EmailMessage message)
//        //{

//        //    var emailConfig = configuration.GetSection("EmailConfiguration");
//        //    var smtpServer = emailConfig["SmtpServer"];
//        //    var port = int.Parse(emailConfig["Port"]);
//        //    var fromEmail = emailConfig["From"];
//        //    var fromEmailPassword = emailConfig["Password"];

//        //    var emailMessage = new MimeMessage();
//        //    emailMessage.From.Add(new MailboxAddress("Course", fromEmail));
//        //    emailMessage.To.AddRange(message.To);
//        //    emailMessage.Subject = message.Subject;
//        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

//        //    return emailMessage;
//        //}

//        //private async Task Send(MimeMessage message)
//        //{
//        //    using var smtpClient = new SmtpClient();
//        //    try
//        //    {
//        //        // Establish a secure connection to SMTP server and authenticate
//        //        await smtpClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
//        //        smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
//        //        await smtpClient.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

//        //        // Send Email
//        //        await smtpClient.SendAsync(message);
//        //    }
//        //    finally
//        //    {
//        //        await smtpClient.DisconnectAsync(true);
//        //    }
//        //}

//        //public async Task SendEmail(EmailMessage message)
//        //{
//        //    var emailMessage = CreateMimeMessage(message);
//        //    await Send(emailMessage);
//        //}

//        //public async void SendEmail(string email, string subject, string messageBody)
//        //{
//        //    var message = new MimeMessage();
//        //    message.From.Add(new MailboxAddress("Course Academy", fromEmail));
//        //    message.To.Add(new MailboxAddress("", email));
//        //    message.Subject = subject;
//        //    message.Body = new TextPart("plain") { Text = messageBody };

//        //    using (var client = new SmtpClient())
//        //    {
//        //        client.Connect(SmtpServer, port, SecureSocketOptions.StartTls);
//        //        client.Authenticate(fromEmail, fromEmailPassword);
//        //        client.Send(message);
//        //        client.Disconnect(true);
//        //    }
//        //}

//    }
//}

