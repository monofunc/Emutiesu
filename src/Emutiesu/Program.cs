using System.Reflection;
using Emutiesu.Configurations;
using Emutiesu.Connector;
using Emutiesu.Mappers;
using Emutiesu.Models;
using Emutiesu.Repositories;
using Emutiesu.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOptions<ProductCsvConnectorOptions>()
    .BindConfiguration("Csv:Products")
    .Validate(x => File.Exists(x.Path), "File not found")
    .ValidateOnStart();

builder.Services.AddTransient<IFileConnector<Product>, CsvConnector<Product, ProductMap, ProductCsvConnectorOptions>>();
builder.Services.AddTransient<IRepository<int, Product>, ProductRepository>();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductReportService, ProductReportService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
