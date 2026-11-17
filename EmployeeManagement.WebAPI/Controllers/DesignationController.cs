using EmployeeManagement.Application.Designations.Commands.CreateDesignation;
using EmployeeManagement.Application.Designations.Dtos;
using EmployeeManagement.Application.Designations.Queries.GetAllDesignations;
using EmployeeManagement.Application.Designations.Queries.GetDesignationById;
using EmployeeManagement.Application.Designations.Commands.DeleteDesignation;
using EmployeeManagement.Application.Designations.Commands.UpdateDesignation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesignationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DesignationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<DesignationDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllDesignationsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DesignationDto>> GetById(int id)
    {
        return Ok(await _mediator.Send(new GetDesignationByIdQuery(id)));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateDesignationCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDesignationCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route id and body id do not match.");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteDesignationCommand(id));
        return NoContent();
    }
}