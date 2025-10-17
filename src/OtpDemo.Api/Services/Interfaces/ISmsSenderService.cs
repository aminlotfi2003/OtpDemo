namespace OtpDemo.Api.Services.Interfaces;

public interface ISmsSenderService
{
    Task SendSmsAsync(string phoneNumber, string message);
}
