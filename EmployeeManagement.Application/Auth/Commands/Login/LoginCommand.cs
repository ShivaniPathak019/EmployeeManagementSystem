using EmployeeManagement.Application.Auth.Dtos;
using MediatR;

namespace EmployeeManagement.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
