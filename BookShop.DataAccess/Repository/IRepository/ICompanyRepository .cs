using BookShop.Models;
using BookShop.Models.ViewModels;

namespace BookShop.DataAccess.Repository.IRepository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company company);
    }
}
