using BookShop.DataAccess.Data;
using BookShop.DataAccess.Repository.IRepository;
using BookShop.DataAccess.Repository.IRepository.IRepository;
using BookShop.Models;
using BookShop.Models.ViewModels;
using System.Linq.Expressions;

namespace BookShop.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db)  : base(db)
        {
            this._db = db;
        }
        public void Update(Company company)
        {
            _db.Companies.Update(company);
        }
        
    }
}
