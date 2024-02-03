namespace PimireWebApp.Utilities
{
    public interface IEmailHelper
    {
        bool Send(string toEmail, string subject, string bodyEmail);
    }
}
