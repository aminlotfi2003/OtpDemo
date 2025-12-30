using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtpDemo.Api.Dtos;
using OtpDemo.Api.Entities;
using OtpDemo.Api.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OtpDemo.Api.Commands.VerifyOtp;

public sealed class VerifyOtpCommandHandler(
    UserManager<ApplicationUser> userManager,
    IOtpService otpService,
    IJwtTokenService jwtTokenService)
    : IRequestHandler<VerifyOtpCommand, VerifyOtpResultDto>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IOtpService _otpService = otpService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    public async Task<VerifyOtpResultDto> Handle(VerifyOtpCommand request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            throw new ValidationException("PhoneNumber is required.");

        if (string.IsNullOrWhiteSpace(request.Code))
            throw new ValidationException("Code is required.");

        var valid = await _otpService.ValidateOtpAsync(request.PhoneNumber, request.Code);
        if (!valid)
            throw new ValidationException("Invalid or expired OTP.");

        var user = await _userManager.Users
            .SingleOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, ct);

        if (user is null)
        {
            user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = request.PhoneNumber,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true,
                LastLoginAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
                throw new ValidationException(string.Join(" | ", createResult.Errors.Select(e => e.Description)));
        }
        else
        {
            user.LastLoginAt = DateTime.UtcNow;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new ValidationException(string.Join(" | ", updateResult.Errors.Select(e => e.Description)));
        }

        var token = _jwtTokenService.GenerateToken(user);
        return new VerifyOtpResultDto(token);
    }
}
