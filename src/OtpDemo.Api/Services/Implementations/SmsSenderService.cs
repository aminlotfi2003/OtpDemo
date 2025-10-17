using OtpDemo.Api.Services.Interfaces;

namespace OtpDemo.Api.Services.Implementations;

public class SmsSenderService : ISmsSenderService
{
    public Task SendSmsAsync(string phoneNumber, string message)
    {
        Console.WriteLine($"SMS to {phoneNumber}: {message}");
        return Task.CompletedTask;
    }
}
