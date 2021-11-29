namespace Emutiesu.Repositories;

public interface IRepository<in TKey, TEntity> where TEntity : class
{
    public IAsyncEnumerable<TEntity> GetAsyncReader();
    public Task<IEnumerable<TEntity>> GetAsync();
    public Task<TEntity> GetAsync(TKey id);
    public Task<TEntity> AddAsync(TEntity entity);
}
