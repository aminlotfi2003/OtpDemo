using MediatR;
using OtpDemo.Api.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OtpDemo.Api.Commands.RequestOtp;

public sealed class RequestOtpCommandHandler(IOtpService otpService)
    : IRequestHandler<RequestOtpCommand, Unit>
{
    private readonly IOtpService _otpService = otpService;

    public async Task<Unit> Handle(RequestOtpCommand request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            throw new ValidationException("PhoneNumber is required.");

        await _otpService.GenerateAndSendOtpAsync(request.PhoneNumber);
        return Unit.Value;
    }
}
