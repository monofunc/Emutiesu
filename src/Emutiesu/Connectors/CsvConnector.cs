using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Emutiesu.Configurations;
using Emutiesu.Mappers;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Threading;

namespace Emutiesu.Connectors;

public class CsvConnector<TEntity, TMap, TOptions> : IFileConnector<TEntity>
    where TMap : ClassMap<TEntity>
    where TOptions : CsvConnectorOptions
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly AsyncReaderWriterLock Lock = new(new JoinableTaskContext());

    private readonly TOptions _options;
    private readonly CsvConfiguration _configuration;

    public CsvConnector(IOptions<TOptions> options)
    {
        _options = options.Value;
        _configuration = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = _options.Delimiter };
    }

    public async IAsyncEnumerable<TEntity> GetReaderAsync()
    {
        using var reader = new StreamReader(_options.Path);
        using var csv = new CsvReader(reader, _configuration);

        csv.Context.RegisterClassMap<TMap>();

        await foreach (var entity in csv.GetRecordsAsync<TEntity>())
        {
            yield return entity;
        }
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await using (await Lock.WriteLockAsync())
        {
            await using var writer = new StreamWriter(_options.Path, true);
            await using var csv = new CsvWriter(writer, _configuration);

            csv.Context.RegisterClassMap<ProductMap>();

            csv.WriteRecord(entity);

            await csv.NextRecordAsync();
        }

        return entity;
    }
}
