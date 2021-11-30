namespace Emutiesu.Connectors;

public interface IFileConnector<TEntity>
{
    IAsyncEnumerable<TEntity> GetReaderAsync();
    Task<TEntity> AddAsync(TEntity entity);
}
