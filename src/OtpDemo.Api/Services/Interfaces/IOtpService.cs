namespace OtpDemo.Api.Services.Interfaces;

public interface IOtpService
{
    Task GenerateAndSendOtpAsync(string phoneNumber);
    Task<bool> ValidateOtpAsync(string phoneNumber, string code);
}
