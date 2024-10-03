using BookShop.DataAccess.Data;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using System.Linq.Expressions;

namespace BookShop.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)  : base(db)
        {
            this._db = db;
        }
        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
        
    }
}
