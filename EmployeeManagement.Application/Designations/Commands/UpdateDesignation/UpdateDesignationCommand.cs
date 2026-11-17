using MediatR;
namespace EmployeeManagement.Application.Designations.Commands.UpdateDesignation;
public record UpdateDesignationCommand(int Id  ,string Title , string? Description): IRequest;

    