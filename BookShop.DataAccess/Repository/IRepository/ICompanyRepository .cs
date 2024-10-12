using BookShop.Models;

namespace BookShop.DataAccess.Repository.IRepository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company company);
    }
}
