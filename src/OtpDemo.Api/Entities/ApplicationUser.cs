using Microsoft.AspNetCore.Identity;

namespace OtpDemo.Api.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    // Custom properties
    public DateTime? LastLoginAt { get; set; }
    // No password needed!
}
