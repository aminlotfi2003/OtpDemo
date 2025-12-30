using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtpDemo.Api.Commands.RequestOtp;
using OtpDemo.Api.Commands.VerifyOtp;
using OtpDemo.Api.Dtos;

namespace OtpDemo.Api.Controllers;

[ApiController]
[Route("api/otp")]
[Produces("application/json")]
public sealed class OtpController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("request")]
    public new async Task<IActionResult> Request([FromBody] RequestOtpCommand command, CancellationToken ct)
    {
        await _sender.Send(command, ct);
        return NoContent();
    }

    [HttpPost("verify")]
    public async Task<ActionResult<VerifyOtpResultDto>> Verify([FromBody] VerifyOtpCommand command, CancellationToken ct)
        => Ok(await _sender.Send(command, ct));
}
