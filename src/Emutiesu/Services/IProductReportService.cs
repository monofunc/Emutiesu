using Emutiesu.Models;

namespace Emutiesu.Services;

public interface IProductReportService
{
    public Task<List<ProductReportItem>> GenerateReportAsync();
}
