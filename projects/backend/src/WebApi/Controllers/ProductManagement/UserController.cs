using Application.UserManagement.Queries.GetUsers;
using Domain.UserManagement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Base.Validation;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ISender sender, ILogger<UserController> logger) : ControllerBase
{
    #region Queries
    [HttpGet("[action]")]
    [Authorize(Roles = Permission.AmerAntiqueDesign_ProductManagement_Reader)]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersList.Query query, CancellationToken cancellationToken)
    {
        Result<List<User>> response = await sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }
    #endregion Queries

    #region Commands
    #endregion Commands
}
