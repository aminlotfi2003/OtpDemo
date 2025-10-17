using OtpDemo.Api.Entities;

namespace OtpDemo.Api.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user);
}
