using EmployeeManagement.Application.Designations.Commands.CreateDesignation;
using EmployeeManagement.Application.Designations.Dtos;
using EmployeeManagement.Application.Designations.Queries.GetAllDesignations;
using EmployeeManagement.Application.Designations.Queries.GetDesignationById;
using EmployeeManagement.Application.Designations.Commands.DeleteDesignation;
using EmployeeManagement.Application.Designations.Commands.UpdateDesignation;
using EmployeeManagement.Application.Designations.Commands.UpdateDesignationPatch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.WebAPI.Controllers;

[ApiController]
[Route("api/designations")]
public class DesignationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DesignationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin,Employee")]
    [HttpGet]
    public async Task<ActionResult<List<DesignationDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllDesignationsQuery()));
    }

    [Authorize(Roles = "Admin,Employee")]
    [HttpGet("{id}")]
    public async Task<ActionResult<DesignationDto>> GetById(int id)
    {
        return Ok(await _mediator.Send(new GetDesignationByIdQuery(id)));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateDesignationCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDesignationCommand command)
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
        await _mediator.Send(new DeleteDesignationCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Partial update – only the fields supplied in the body will be changed.
    /// Send {@code null} for any property you wish to keep unchanged.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<DesignationDto>> PartialUpdate(
        int id,
        [FromBody] UpdateDesignationPatchCommand command)
    {
        command.Id = id;
        var dto = await _mediator.Send(command);
        return Ok(dto);
    }
}