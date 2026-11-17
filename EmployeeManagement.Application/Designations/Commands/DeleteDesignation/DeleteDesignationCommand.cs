using MediatR;

namespace EmployeeManagement.Application.Designations.Commands.DeleteDesignation;

public record DeleteDesignationCommand(int Id) : IRequest;