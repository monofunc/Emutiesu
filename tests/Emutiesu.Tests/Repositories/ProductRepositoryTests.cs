using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emutiesu.Connectors;
using Emutiesu.Exceptions;
using Emutiesu.Models;
using Emutiesu.Repositories;
using Xunit;

namespace Emutiesu.Tests.Repositories;

public class ProductRepositoryTests
{
    [Fact]
    public async Task GetAsync()
    {
        var products = new List<Product>
        {
            new() { Id = 1, Article = 69, Price = 420 },
            new() { Id = 2, Article = 69, Price = 240 },
        };

        var repository = new ProductRepository(new TestConnector(products));

        Assert.Equal(products[0], await repository.GetAsync(1));
    }

    [Fact]
    public async Task AddAsync()
    {
        var products = new List<Product>
        {
            new() { Id = 1, Article = 69, Price = 420 },
            new() { Article = 69, Price = 240 },
        };

        var repository = new ProductRepository(new TestConnector());

        foreach (var product in products)
        {
            await repository.AddAsync(product);
        }

        Assert.Equal(products[0], await repository.GetAsync(1));
    }

    [Fact]
    public async Task ThrowsExceptionWhenWritingProductWithExistingIdentifier()
    {
        var products = new List<Product>
        {
            new() { Id = 1 }
        };

        var repository = new ProductRepository(new TestConnector(products));

        await Assert.ThrowsAsync<EntityDuplicateIdentifierException>(() => repository.AddAsync(products.First()));
    }

    [Fact]
    public async Task ThrowsExceptionWhenEntityWithSpecifiedIdentifierNotFound()
    {
        var repository = new ProductRepository(new TestConnector());

        await Assert.ThrowsAsync<InvalidOperationException>(() => repository.GetAsync(1));
    }
}

public class TestConnector : IFileConnector<Product>
{
    private readonly List<Product> _products = new();

    public TestConnector()
    {
    }

    public TestConnector(List<Product> products)
    {
        _products = products;
    }

    public IAsyncEnumerable<Product> GetReaderAsync()
    {
        return _products.ToAsyncEnumerable();
    }

    public async Task<Product> AddAsync(Product entity)
    {
        _products.Add(entity);

        return await Task.FromResult(entity);
    }
}
