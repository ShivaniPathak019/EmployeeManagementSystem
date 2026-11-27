using EmployeeManagement.Application.Auth.Dtos;
using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly AuthService _authService;

    public LoginCommandHandler(AuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(
        LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request, cancellationToken);
    }
}
