using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.User.ActiveUser;
using Portfolio.Application.Features.User.CreateUser;
using Portfolio.Application.Features.User.DeleteUser;
using Portfolio.Application.Features.User.UpdateUser;
using Portfolio.Application.Features.User.UpdateUserRole;

namespace Portfolio.API.Controllers;
[Authorize(Roles = "admin")]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] CreateUserRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("updateUsername")]
    public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("updateRole")]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPut("active")]
    public async Task<IActionResult> ActiveUser([FromBody] ActiveUserRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}