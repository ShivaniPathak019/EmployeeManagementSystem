using EmployeeManagement.Application.Designations.Dtos;

using MediatR;

namespace EmployeeManagement.Application.Designations.Queries.GetAllDesignations;

public record GetAllDesignationsQuery : IRequest<List<DesignationDto>>;