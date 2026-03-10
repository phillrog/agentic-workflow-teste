using Domain;

namespace Application;

public class ProductService
{
    public bool ValidateProduct(Product product)
    {
        return product.IsValid();
    }
}