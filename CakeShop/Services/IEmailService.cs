using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace CakeShop.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body, string recipientEmail);
    }
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string subject, string body, string recipientEmail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("CakeShop", "uitcakeshop@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", recipientEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("uitcakeshop@gmail.com", "nseg stpi junf muqx");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
