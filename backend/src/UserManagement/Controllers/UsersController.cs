using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Features.UserManagement.Commands;
using UserManagement.Features.UserManagement.DTOs;
using UserManagement.Features.UserManagement.Queries;

namespace UserManagement.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(
    IMediator _mediator
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await _mediator.Send(new GetUserDetailQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateUser), new
            {
                Message = "Create user successfully",
                Id = result
            });

        } catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
     }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditUser(Guid id,
        [FromBody] UserDto dto)
    {
        try
        {
            var result = await _mediator.Send(new EditUserCommand
            (
                id,
                dto.Username,
                dto.Status
            ));
            return NoContent();

        } catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
