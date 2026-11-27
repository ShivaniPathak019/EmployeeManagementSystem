using EmployeeManagement.Application.Departments.Dtos;
using EmployeeManagement.Application.Departments.Queries.GetAllDepartments;
using EmployeeManagement.Application.Departments.Commands.CreateDepartment;
using EmployeeManagement.Application.Departments.Queries.GetDepartmentById;
using EmployeeManagement.Application.Departments.Commands.UpdateDepartment;
using EmployeeManagement.Application.Departments.Commands.DeleteDepartment;
using EmployeeManagement.Application.Departments.Commands.UpdateDepartmentPatch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers;

[ApiController]
[Route("api/departments")]
//controller can be controller name-Controller 
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin,Employee")]
    [HttpGet]
    public async Task<ActionResult<List<DepartmentDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllDepartmentsQuery());
        return Ok(result);
    }



    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateDepartmentCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }



    [Authorize(Roles = "Admin,Employee")]
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetDepartmentByIdQuery(id));
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDepartmentCommand command)
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
        await _mediator.Send(new DeleteDepartmentCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Partial update – only the fields supplied in the body will be changed.
    /// Send {@code null} for any property you wish to keep unchanged.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<DepartmentDto>> PartialUpdate(
    int id,
    [FromBody] UpdateDepartmentPatchCommand command)
    {
        command.Id = id;

        var dto = await _mediator.Send(command);

        return Ok(dto);
    }
}