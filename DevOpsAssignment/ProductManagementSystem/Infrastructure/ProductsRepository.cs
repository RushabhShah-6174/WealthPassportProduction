using Amazon.DynamoDBv2.DataModel;
using ProductManagementSystem.Domain;
using ProductManagementSystem.Interfaces;

namespace ProductManagementSystem.Infrastructure;

/// <summary>
/// Repository for products.
/// </summary>
/// <seealso cref="ProductManagementSystem.Interfaces.IProductsRepository" />
public class ProductsRepository(IDynamoDBContext dynamoDBContext) : IProductsRepository
{
    public IDynamoDBContext DynamoDBContext { get; } = dynamoDBContext;

    /// <inheritdoc/>
    public async Task<Product> CreateAsync(Product product)
    {
        if (string.IsNullOrEmpty(product.Id))
        {
            product.Id = Guid.NewGuid().ToString();
        }

        await DynamoDBContext.SaveAsync(product);
        return product;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(string id)
    {
        await DynamoDBContext.DeleteAsync<Product>(id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await DynamoDBContext.ScanAsync<Product>(new List<ScanCondition>()).GetRemainingAsync();
    }

    /// <inheritdoc/>
    public async Task<Product> GetByIdAsync(string id)
    {
        return await DynamoDBContext.LoadAsync<Product>(id);
    }

    /// <inheritdoc/>
    public async Task<Product> UpdateAsync(Product product)
    {
        await DynamoDBContext.SaveAsync<Product>(product);
        return product;
    }
}
