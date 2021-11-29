namespace Emutiesu.Configurations;

public abstract class CsvConnectorOptions
{
    public string Path { get; init; } = string.Empty;
    public string Delimiter { get; init; } = ";";
}
