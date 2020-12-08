using Domain.Entities;
using System.Linq;

namespace Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);

    }
}
