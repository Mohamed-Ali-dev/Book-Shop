using BookShop.Models;

namespace BookShop.DataAccess.Repository.IRepository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
