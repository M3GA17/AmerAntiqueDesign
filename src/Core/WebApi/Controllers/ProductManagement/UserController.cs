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

    [HttpGet("test-role")]
    [Authorize(Roles = Permission.AmerAntiqueDesign_ProductManagement_Reader)]
    public IActionResult TestRoleAccess()
    {
        // Se arrivi qui, l'autorizzazione basata su ruoli funziona.
        return Ok("ACCESSO CONSENTITO! Il ruolo è stato riconosciuto.");
    }
    //[HttpGet("[action]")] 
    //[ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    //{
    //    GetCategoriesListQuery query = new();
    //    Result<List<Category>> response = await sender.Send(query, cancellationToken);
    //    return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    //}
    #endregion Queries

    #region Commands
    //[HttpPost("[action]")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    //{
    //    Result response = await sender.Send(command, cancellationToken);
    //    return response.IsSuccess ? Created() : BadRequest(response.Error);
    //}

    //[HttpPost("[action]")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
    //{
    //    Result response = await sender.Send(command, cancellationToken);
    //    return response.IsSuccess ? Created() : BadRequest(response.Error);
    //}
    #endregion Commands
}
