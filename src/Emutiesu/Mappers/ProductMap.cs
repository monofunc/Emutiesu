using CsvHelper.Configuration;
using Emutiesu.Models;
using JetBrains.Annotations;

namespace Emutiesu.Mappers;

[UsedImplicitly]
public sealed class ProductMap : ClassMap<Product>
{
    public ProductMap()
    {
        Map(x => x.Id).Name("Id");
        Map(x => x.Type).Name("Type");
        Map(x => x.PrimaryCategory).Name("Category 1");
        Map(x => x.SecondaryCategory).Name("Category 2");
        Map(x => x.Article).Name("Article number");
        Map(x => x.Title).Name("Title");
        Map(x => x.Description).Name("Description");
        Map(x => x.Material).Name("Material");
        Map(x => x.Size).Name("Size");
        Map(x => x.Color).Name("Color");
        Map(x => x.Price).Name("Price");
    }
}
