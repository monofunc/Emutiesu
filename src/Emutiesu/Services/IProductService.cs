using Emutiesu.Models;

namespace Emutiesu.Services;

public interface IProductService
{
    public Task<List<Product>> SearchAsync(string searchQuery);
    public Task<Product> GetAsync(int id);
    public Task<Product> AddAsync(Product product);
}
