using MediatR;
using OtpDemo.Api.Dtos;

namespace OtpDemo.Api.Commands.VerifyOtp;

public sealed record VerifyOtpCommand(string PhoneNumber, string Code) : IRequest<VerifyOtpResultDto>;
