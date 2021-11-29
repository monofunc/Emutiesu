using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Emutiesu.Models;
using Emutiesu.Repositories;
using Emutiesu.Services;
using Xunit;

namespace Emutiesu.Tests.Services;

public class ProductReportServiceTests
{
    [Fact]
    public async Task GenerateReportSuccessfullyAsync()
    {
        var products = new List<Product>
        {
            new() { Id = 1, Type = "Watch", PrimaryCategory = "Russian", SecondaryCategory = "For Men", Price = 10 },
            new() { Id = 2, Type = "Watch", PrimaryCategory = "Russian", SecondaryCategory = "For Men", Price = 10 },
            new() { Id = 3, Type = "Watch", PrimaryCategory = "Chinese", SecondaryCategory = "For Men", Price = 10 },
            new() { Id = 4, Type = "T-Shirt", PrimaryCategory = "Russian", SecondaryCategory = "For Men", Price = 20 },
            new() { Id = 5, Type = "T-Shirt", PrimaryCategory = "Russian", SecondaryCategory = "For Woman", Price = 30 },
            new() { Id = 5, Type = "Cup", PrimaryCategory = "Russian", SecondaryCategory = "Unisex", Price = 40 }
        };

        var expected = new List<ProductReportItem>
        {
            new() { Type = "Watch", PrimaryCategory = "Russian", SecondaryCategory = "For Men", Quantity = 2, Amount = 20 },
            new() { Type = "Watch", PrimaryCategory = "Chinese", SecondaryCategory = "For Men", Quantity = 1, Amount = 10 },
            new() { Type = "T-Shirt", PrimaryCategory = "Russian", SecondaryCategory = "For Men", Quantity = 1, Amount = 20 },
            new() { Type = "T-Shirt", PrimaryCategory = "Russian", SecondaryCategory = "For Woman", Quantity = 1, Amount = 30 },
            new() { Type = "Cup", PrimaryCategory = "Russian", SecondaryCategory = "Unisex", Quantity = 1, Amount = 40 }
        };

        var generator = new ProductReportService(new TestRepository(products));

        var report = await generator.GenerateReportAsync();

        Assert.Equal(JsonSerializer.Serialize(report), JsonSerializer.Serialize(expected));
    }

    [Fact]
    public async Task ShouldReturnEmptyReportWhenThereIsNoProductsAsync()
    {
        var generator = new ProductReportService(new TestRepository());

        var report = await generator.GenerateReportAsync();

        Assert.Equal(new List<ProductReportItem>().Count, report.Count);
    }
}

public class TestRepository : IRepository<int, Product>
{
    private readonly List<Product> _products = new();

    public TestRepository()
    {
    }

    public TestRepository(List<Product> products)
    {
        _products = products;
    }

    public IAsyncEnumerable<Product> GetAsyncReader()
    {
        return _products.ToAsyncEnumerable();
    }

    public async Task<IEnumerable<Product>> GetAsync()
    {
        return await _products.ToAsyncEnumerable().ToListAsync();
    }

    public async Task<Product> GetAsync(int id)
    {
        return await _products.ToAsyncEnumerable().FirstAsync(x => x.Id == id);
    }

    public async Task<Product> AddAsync(Product entity)
    {
        _products.Add(entity);

        return await Task.FromResult(entity);
    }
}
