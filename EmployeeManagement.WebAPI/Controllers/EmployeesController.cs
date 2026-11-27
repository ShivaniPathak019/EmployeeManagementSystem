using EmployeeManagement.Application.Employees.Dtos;
using EmployeeManagement.Application.Employees.Queries.GetAllEmployees;
using EmployeeManagement.Application.Employees.Queries.GetEmployeeById;
using EmployeeManagement.Application.Employees.Commands.CreateEmployee;
using EmployeeManagement.Application.Employees.Commands.DeleteEmployee;
using EmployeeManagement.Application.Employees.Commands.UpdateEmployee;
using EmployeeManagement.Application.Employees.Commands.UpdateEmployeePatch;
using EmployeeManagement.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin,Employee")]
    [HttpGet]
    public async Task<ActionResult<PagedResult<EmployeeDto>>> GetAll(
    [FromQuery] GetAllEmployeesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Employee")]
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateEmployeeCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route id and body id do not match.");

        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Partial update – only the fields supplied in the body will be changed.
    /// Send {@code null} for any property you wish to keep unchanged.
    /// </summary>
    [Authorize(Roles = "Admin,Employee")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<EmployeeDto>> PartialUpdate(
        int id,
        [FromBody] UpdateEmployeePatchCommand command)
    {
        // Route ID takes precedence; if body also contains id it will be overridden
        command.Id = id;
        var dto = await _mediator.Send(command);
        return Ok(dto);
    }
}