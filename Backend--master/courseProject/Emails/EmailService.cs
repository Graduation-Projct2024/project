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
using courseProject.Emails;


namespace courseProject.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }


        public async Task SendVerificationEmail(string ToEmail,string Subject ,  string Body)
        {
            using (var client = new SmtpClient(_emailConfig.SmtpServer, _emailConfig.Port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_emailConfig.From, _emailConfig.Password);
                var fromAddress = new MailAddress(_emailConfig.From, "Course Academy");
                var message = new MailMessage()
                {
                    From = fromAddress,
                    Subject = Subject,
                    IsBodyHtml = true,
                    Body = Body,
                };
                message.To.Add(new MailAddress(ToEmail));


                try
                {
                    await client.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    // Handle error
                    throw ex;
                }
            };
        }
    }
        }


       

    


