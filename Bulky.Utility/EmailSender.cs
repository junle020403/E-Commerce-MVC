using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace BulkyBook.Utility {
    public class EmailSender : IEmailSender {
		private readonly string smtpServer;
		private readonly int smtpPort;
		private readonly string fromEmailAddress;
		private readonly string senderPassword;

		public string SendGridSecret { get; set; }

        public EmailSender(string smtpServer, int smtpPort, string fromEmailAddress, string senderPassword) {
			this.smtpServer = smtpServer;
			this.smtpPort = smtpPort;
			this.fromEmailAddress = fromEmailAddress;
			this.senderPassword = senderPassword;
		}

        public Task SendEmailAsync(string email, string subject, string htmlMessage) {
            //logic to send email

            //var client = new SendGridClient(fromEmailAddress);

            //var from = new EmailAddress("hello@dotnetmastery.com", "Bulky Book");
            //var to = new EmailAddress(email);
            //var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            //return client.SendEmailAsync(message);

			var message = new MailMessage
			{
				From = new MailAddress(fromEmailAddress),
				Subject = subject,
				Body = htmlMessage,
				IsBodyHtml = true
			};

			message.To.Add(new MailAddress(email));

			using var client = new SmtpClient(smtpServer, smtpPort);
			client.UseDefaultCredentials = false;
			client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, senderPassword);
			client.EnableSsl = true; // set to true if using SSL/TLS
			client.Send(message);

			return Task.CompletedTask;
		}
    }
}

