using EmployeeManagement.Application.Departments.Dtos;
using EmployeeManagement.Application.Departments.Queries.GetAllDepartments;
using EmployeeManagement.Application.Departments.Commands.CreateDepartment;
using EmployeeManagement.Application.Departments.Queries.GetDepartmentById;
using EmployeeManagement.Application.Departments.Commands.UpdateDepartment;
using EmployeeManagement.Application.Departments.Commands.DeleteDepartment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllDepartmentsQuery());
        return Ok(result);
    }



    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateDepartmentCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetDepartmentByIdQuery(id));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDepartmentCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route id and body id do not match.");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteDepartmentCommand(id));
        return NoContent();
    }
}