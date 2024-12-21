using CNTT36_WebXemPhim.Repository.IRepository;
using System.Net.Mail;
using System.Net;
namespace CNTT36_WebXemPhim.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _config;
        public EmailRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailSettings = _config.GetSection("EmailSettings");
            using var client = new SmtpClient(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]))
            {
                Credentials = new NetworkCredential(emailSettings["SenderEmail"], emailSettings["AppPassword"]),
                EnableSsl = true
            };
            var mail = new MailMessage
            {
                From = new MailAddress(emailSettings["SenderEmail"], emailSettings["SenderName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mail.To.Add(toEmail);
            await client.SendMailAsync(mail);
        }
    }
}