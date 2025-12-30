using MediatR;

namespace OtpDemo.Api.Commands.RequestOtp;

public sealed record RequestOtpCommand(string PhoneNumber) : IRequest<Unit>;
