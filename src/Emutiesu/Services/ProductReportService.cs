using Emutiesu.Models;
using Emutiesu.Repositories;

namespace Emutiesu.Services;

public class ProductReportService : IProductReportService
{
    private readonly IRepository<int, Product> _productRepository;

    public ProductReportService(IRepository<int, Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductReportItem>> GenerateReportAsync()
    {
        var products = await _productRepository.GetAsync();

        return products
            .GroupBy(x => new { x.Type, x.PrimaryCategory, x.SecondaryCategory })
            .Select(x => new ProductReportItem
            {
                Type = x.Key.Type,
                PrimaryCategory = x.Key.PrimaryCategory,
                SecondaryCategory = x.Key.SecondaryCategory,
                Quantity = x.Count(),
                Amount = x.Sum(product => product.Price)
            })
            .ToList();
    }
}
