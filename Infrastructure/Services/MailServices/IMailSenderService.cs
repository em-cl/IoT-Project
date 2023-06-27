namespace Infrastructure.Services.MailServices;

public interface IMailSenderService
{
    Task<bool> SendAsync(string emailTo, string subject, string body);
}