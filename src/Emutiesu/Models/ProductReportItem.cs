namespace Emutiesu.Models;

/// <summary>
///     Row Representation for Product Report
/// </summary>
public class ProductReportItem
{
    /// <summary>
    ///     Type
    /// </summary>
    public string Type { get; init; } = string.Empty;

    /// <summary>
    ///     Primary Category
    /// </summary>
    public string PrimaryCategory { get; init; } = string.Empty;

    /// <summary>
    ///     Secondary Category
    /// </summary>
    public string SecondaryCategory { get; init; } = string.Empty;

    /// <summary>
    ///     Aggregated Quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    ///     Aggregated Amount
    /// </summary>
    public decimal Amount { get; set; }
}
