namespace Emutiesu.Models;

/// <summary>
///     Abstract Product that will be sold in Abstract Shop (maybe)
/// </summary>
public class Product
{
    /// <summary>
    ///     Identifier
    /// </summary>
    /// <example>1</example>
    public int? Id { get; set; }

    /// <summary>
    ///     Type
    /// </summary>
    /// <example>Часы</example>
    public string Type { get; init; } = string.Empty;

    /// <summary>
    ///     Primary Category
    /// </summary>
    /// <example>Мужские</example>
    public string PrimaryCategory { get; init; } = string.Empty;

    /// <summary>
    ///     Secondary Category
    /// </summary>
    /// <example>Футболка</example>
    public string SecondaryCategory { get; init; } = string.Empty;

    /// <summary>
    ///     Article Number
    /// </summary>
    public int Article { get; init; }

    /// <summary>
    ///     Title
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    ///     Description
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    ///     Used material
    /// </summary>
    public string? Material { get; init; }

    /// <summary>
    ///     Size
    /// </summary>
    public string? Size { get; init; }

    /// <summary>
    ///     Color
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    ///     Price
    /// </summary>
    public decimal Price { get; set; }
}
