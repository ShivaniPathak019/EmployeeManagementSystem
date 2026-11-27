using EmployeeManagement.Application.Auth.Dtos;
using EmployeeManagement.Application.Services;
using MediatR;

namespace EmployeeManagement.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly AuthService _authService;

    public RegisterCommandHandler(AuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(
        RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request, cancellationToken);
    }
}
