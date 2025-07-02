using Application.ProductManagement.Commands.CreateCategory;
using Application.ProductManagement.Commands.CreateProduct;
using Application.ProductManagement.Queries.GetProducts;
using Domain.ProductManagement;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Base.Validation;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ISender sender, ILogger<ProductController> logger) : ControllerBase
{
    #region Queries
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        GetProductListQuery query = new();
        Result<List<Product>> response = await sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        GetCategoriesListQuery query = new();
        Result<List<Category>> response = await sender.Send(query, cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }
    #endregion Queries

    #region Commands
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        Result response = await sender.Send(command, cancellationToken);
        return response.IsSuccess ? Created() : BadRequest(response.Error);
    }

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        Result response = await sender.Send(command, cancellationToken);
        return response.IsSuccess ? Created() : BadRequest(response.Error);
    }
    #endregion Commands


    //[HttpPost]
    //[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> Register([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    //{
    //    Result<CreateUserCommand.Response> response = await sender.Send(command, cancellationToken);
    //    return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    //}



    //[HttpGet("getCategoryList")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetCategoryList([FromRoute] GetCategoryListQuery command, CancellationToken cancellationToken)
    //{
    //    Result<GetCategoryListQuery.Response> response = await sender.Send(command, cancellationToken);
    //    return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    //}








    //[HttpPost("login")]
    //[ProducesResponseType(typeof(LogInUserCommand.Response), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> Login([FromBody] LogInUserCommand command, CancellationToken cancellationToken)
    //{
    //    Result<LogInUserCommand.Response> response = await sender.Send(command, cancellationToken);
    //    return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    //}


    //[HttpPost]
    //[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> Register([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    //{
    //    Result<CreateUserCommand.Response> response = await sender.Send(command, cancellationToken);
    //    return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    //}

    //[HttpGet]
    //[ProducesResponseType(typeof(List<GetUserQuery.Response>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetUsers([FromBody] GetUserQuery query, CancellationToken cancellationToken)
    //{
    //    Result<List<GetArticleQuery.Response>> response = await sender.Send(query, cancellationToken);
    //    return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    //}

    //[HttpGet("{Id:Guid}")]
    //[ProducesResponseType(typeof(List<GetArticleQuery.Response>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> GetArticle(Guid id, CancellationToken cancellationToken)
    //{
    //    GetArticleQuery query = new(id);
    //    Result<List<GetArticleQuery.Response>> response = await sender.Send(query, cancellationToken);
    //    return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    //}
}
