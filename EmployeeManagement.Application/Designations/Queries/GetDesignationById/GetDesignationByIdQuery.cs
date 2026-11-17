using EmployeeManagement.Application.Designations.Dtos;
using MediatR;

namespace EmployeeManagement.Application.Designations.Queries.GetDesignationById;

public record GetDesignationByIdQuery(int Id) : IRequest<DesignationDto>;