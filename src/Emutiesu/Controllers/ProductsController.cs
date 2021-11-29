using Emutiesu.Models;
using Emutiesu.Services;
using Microsoft.AspNetCore.Mvc;

namespace Emutiesu.Controllers;

[Produces("application/json")]
[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    ///     Search Product by its title
    /// </summary>
    /// <param name="search">Title or part of it to search</param>
    /// <response code="200">Collection of Products that satisfy expression</response>
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<Product>>> FindProductsByTitleAsync(string search)
    {
        return await _productService.SearchAsync(search);
    }

    /// <summary>
    ///     Get Product by its identifier
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns Product representation</response>
    /// <response code="404">Returns when Product not found</response>
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id)
    {
        try
        {
            return await _productService.GetAsync(id);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    /// <summary>
    ///     Create Product
    /// </summary>
    /// <param name="product">Product object</param>
    /// <response code="200">Created Product representation</response>
    /// <response code="400">One or more validation error happened</response>
    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> CreateProductAsync(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _productService.AddAsync(product);

            return CreatedAtAction("GetProduct", result);
        }
        catch (InvalidOperationException)
        {
            return BadRequest();
        }
    }
}
