using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emutiesu.Controllers;
using Emutiesu.Models;
using Emutiesu.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Emutiesu.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Title = "Casio", Type = "Watch", PrimaryCategory = "Russian", SecondaryCategory = "For Men", Price = 10 },
        new Product { Id = 2, Title = "Casablanca", Type = "Watch", PrimaryCategory = "Russian", SecondaryCategory = "For Men", Price = 10 },
        new Product { Id = 3, Title = "Durandal", Type = "Watch", PrimaryCategory = "Chinese", SecondaryCategory = "For Men", Price = 10 },
        new Product { Id = 4, Title = "Colin's", Type = "T-Shirt", PrimaryCategory = "Russian", SecondaryCategory = "For Men", Price = 20 },
        new Product { Id = 5, Title = "ZARA", Type = "T-Shirt", PrimaryCategory = "Russian", SecondaryCategory = "For Woman", Price = 30 },
        new Product { Id = 5, Title = "Tesla", Type = "Cup", PrimaryCategory = "Russian", SecondaryCategory = "Unisex", Price = 40 }
    };

    [Fact]
    public async Task FindProductsByTitleReturnsExactlyOneElementAsync()
    {
        var controller = new ProductsController(new TestProductService(_products));

        var response = await controller.FindProductsByTitleAsync("ZA");

        Assert.Equal(1, response.Value?.Count);
    }

    [Fact]
    public async Task FindProductsByTitleReturnsExactlyTwoElementAsync()
    {
        var controller = new ProductsController(new TestProductService(_products));

        var response = await controller.FindProductsByTitleAsync("Cas");

        Assert.Equal(2, response.Value?.Count);
    }

    [Fact]
    public async Task GetReturnNotFoundIfNoElementWithSpecifiedIdentifierAsync()
    {
        var controller = new ProductsController(new TestProductService(_products));

        var response = await controller.GetProductAsync(51);

        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task ValidationErrorOnAddAsync()
    {
        var controller = new ProductsController(new TestProductService());

        controller.ModelState.AddModelError("Error", "Bad Request");

        var product = new Product { Id = 1 };

        var response = await controller.CreateProductAsync(product);

        Assert.IsType<BadRequestObjectResult>(response.Result);
    }
}

public class TestProductService : IProductService
{
    private readonly List<Product> _products = new();

    public TestProductService()
    {
    }

    public TestProductService(List<Product> products)
    {
        _products = products;
    }

    public async Task<List<Product>> SearchAsync(string searchQuery)
    {
        return await _products.ToAsyncEnumerable().Where(x => x.Title.Contains(searchQuery)).ToListAsync();
    }

    public async Task<Product> GetAsync(int id)
    {
        return await _products.ToAsyncEnumerable().FirstAsync(x => x.Id == id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        _products.Add(product);

        return await Task.FromResult(product);
    }
}
