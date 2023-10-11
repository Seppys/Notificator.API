using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Notificator.API.Models;
using MailKit.Net.Smtp;

namespace Notificator.API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
       
       [HttpPost("send")]
       public IActionResult sendEmail(Email email)
        {
            var message = createMessage(email.Address, email.Subject, email.Body);

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587);
                smtp.Authenticate(Environment.GetEnvironmentVariable("GMAIL_ADDRESS"), Environment.GetEnvironmentVariable("GMAIL_TOKEN"));

                if (!smtp.IsConnected || !smtp.IsAuthenticated)
                    return StatusCode(500);

                smtp.Send(message);

                smtp.Disconnect(true);

                return Ok();
            }
        }

        private MimeMessage createMessage(string address, string subject, string? body)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Notificator API", Environment.GetEnvironmentVariable("GMAIL_ADDRESS")));
            message.To.Add(new MailboxAddress(null, address));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            return message;
        }
    }
}
