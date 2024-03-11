using Entities;
namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
        Task<IEnumerable<Company>> GetByIDsAsync(IEnumerable<Guid> Ids, bool trackChanges);
        void DeleteCompany(Company company);
    }
}