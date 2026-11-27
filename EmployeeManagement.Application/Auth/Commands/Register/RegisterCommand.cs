using EmployeeManagement.Application.Auth.Dtos;
using MediatR;

namespace EmployeeManagement.Application.Auth.Commands.Register;
// this creates a register command 
public record RegisterCommand(string Email, string Password) : IRequest<AuthResponseDto>;
// after creating command the control goes to ValidationBehavior.cs 