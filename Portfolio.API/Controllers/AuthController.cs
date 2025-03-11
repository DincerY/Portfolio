using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Auth.Login;

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

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var a = await _mediator.Send(request);
        return Ok(a);
    }
}