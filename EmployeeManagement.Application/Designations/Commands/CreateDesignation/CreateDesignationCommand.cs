using MediatR;

namespace EmployeeManagement.Application.Designations.Commands.CreateDesignation;

public record CreateDesignationCommand(string Title, string? Description) : IRequest<int>;