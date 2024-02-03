using PimireWebApp.Models;
using System.Net;
using System.Net.Mail;

namespace PimireWebApp.Utilities
{
    public class EmailHelper: IEmailHelper
    {
        private readonly IConfiguration _configuration;
        private readonly PimireDbContext _pimireDbContext;
        public EmailHelper(IConfiguration configuration, PimireDbContext pimireDbContext)
        {
            _configuration = configuration;
            _pimireDbContext = pimireDbContext;
        }
        public bool Send(string toEmail,string subject,string bodyEmail)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_configuration.GetSection("EmailConfiguration:FromEmail").Value??"");
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = bodyEmail;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = _configuration.GetSection("EmailConfiguration:Host").Value ?? "smtp.gmail.com";
                smtpClient.Port = string.IsNullOrEmpty(_configuration.GetSection("EmailConfiguration:Port").Value) ? 587 : Convert.ToInt32(_configuration.GetSection("EmailConfiguration:Port").Value);
                
                smtpClient.Credentials = new NetworkCredential(_configuration.GetSection("EmailConfiguration:UserName").Value, _configuration.GetSection("EmailConfiguration:Password").Value);
                smtpClient.EnableSsl = false;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    ErrorMessage = ex.Message,
                    CustomMessage = "ToEmail :" + toEmail + ", Subject: " + subject + " BodyEmail: " + bodyEmail,
                    CreatedDate = DateTime.Now,
                };
                _pimireDbContext.ErrorLogs.Add(errorLog);
                _pimireDbContext.SaveChanges();
                return false;
            }
        }
    }
}
