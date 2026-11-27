using EmployeeManagement.Application.Auth.Commands.Login;
using EmployeeManagement.Application.Auth.Commands.Register;
using EmployeeManagement.Application.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    // Dependency Injection using constructor 
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    // 
    [HttpPost("register")]
    [AllowAnonymous]
    // calls the RegisterCommand.cs
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterCommand command) => Ok(await _mediator.Send(command));
    // calls the  LoginCommand.cs 
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginCommand command)
        => Ok(await _mediator.Send(command));
}
