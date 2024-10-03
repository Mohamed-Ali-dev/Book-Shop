using BookShop.Models;

namespace BookShop.DataAccess.Repository.IRepository
{
    public interface IProductImageRepository : IRepository<ProductImage>
    { 
        void Update(ProductImage obj);
      
    }
}
