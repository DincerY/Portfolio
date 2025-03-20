using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Auth.Login;
using Portfolio.Application.Features.Auth.Refresh;
using Portfolio.Application.Features.Auth.Register;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}