using BookShop.DataAccess.Data;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using System.Linq.Expressions;

namespace BookShop.DataAccess.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductImageRepository(ApplicationDbContext db)  : base(db)
        {
            this._db = db;
        }
        public void Update(ProductImage obj)
        {
            _db.ProductImages.Update(obj);
        }
        
    }
}
