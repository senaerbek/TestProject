using MimeKit;
using TestProject.Models;
using MailKit.Net.Smtp;

namespace TestProject.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfigurations config;
        public EmailSender(EmailConfigurations config)
        {
            this.config = config;
        }

        public void SendEmail(Message message)
        {
            var emailmesage = CreateEmailMessage(message);
            Send(emailmesage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(config.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }
        private void Send(MimeMessage message){
            using (var c = new SmtpClient())
            {
                try
                {
                    c.Connect("smtp.gmail.com", 465,true);
                    c.AuthenticationMechanisms.Remove("XOAUTH2");
                    c.Authenticate(config.UserName, config.Password);
                    System.Console.WriteLine(config.UserName);
                    c.Send(message);
                    
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
                finally
                {
                    c.Disconnect(true);
                    c.Dispose();
                }
            }
        }
        
    }
}