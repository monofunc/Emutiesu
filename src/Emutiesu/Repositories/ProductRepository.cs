using Emutiesu.Connector;
using Emutiesu.Exceptions;
using Emutiesu.Models;

namespace Emutiesu.Repositories;

public class ProductRepository : IRepository<int, Product>
{
    private static int _currentId;

    private readonly IFileConnector<Product> _connector;

    public ProductRepository(IFileConnector<Product> connector)
    {
        _connector = connector;
    }

    public IAsyncEnumerable<Product> GetAsyncReader()
    {
        return _connector.GetReaderAsync();
    }

    public async Task<IEnumerable<Product>> GetAsync()
    {
        return await _connector.GetReaderAsync().ToListAsync();
    }

    public async Task<Product> GetAsync(int id)
    {
        return await _connector.GetReaderAsync().FirstAsync(x => x.Id == id);
    }

    public async Task<Product> AddAsync(Product entity)
    {
        if (entity.Id is not null && !await ValidateIdAsync(entity.Id))
        {
            throw new EntityDuplicateIdentifierException();
        }

        entity.Id ??= await GenerateIdAsync();

        return await _connector.AddAsync(entity);
    }

    private async Task<int> GenerateIdAsync()
    {
        // Used only on first run
        if (_currentId == 0)
        {
            Interlocked.Exchange(ref _currentId, await GetMaxIdAsync());
        }

        // Since somebody can put Product with Id > _currentId, we loop until we can get available Id
        // Usually we can get available Id on the first iteration
        while (true)
        {
            Interlocked.Increment(ref _currentId);

            if (await ValidateIdAsync(_currentId))
            {
                return _currentId;
            }
        }
    }

    private async Task<int> GetMaxIdAsync()
    {
        var max = await _connector.GetReaderAsync().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

        return max?.Id ?? 0;
    }

    private async Task<bool> ValidateIdAsync(int? id)
    {
        return !await GetAsyncReader().AnyAsync(x => x.Id == id);
    }
}
