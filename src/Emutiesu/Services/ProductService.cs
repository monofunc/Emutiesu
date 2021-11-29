using Emutiesu.Models;
using Emutiesu.Repositories;

namespace Emutiesu.Services;

public class ProductService : IProductService
{
    private readonly IRepository<int, Product> _productRepository;

    public ProductService(IRepository<int, Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> SearchAsync(string searchQuery)
    {
        return await _productRepository.GetAsyncReader()
            .Where(x => x.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
    }

    public async Task<Product> GetAsync(int id)
    {
        return await _productRepository.GetAsync(id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        return await _productRepository.AddAsync(product);
    }
}
