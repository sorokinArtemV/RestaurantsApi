using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.UpdateUser;
using Restaurants.Core.Domain.Constants;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }
}