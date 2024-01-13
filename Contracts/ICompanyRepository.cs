using Entities;
namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
        IEnumerable<Company> GetByIDs(IEnumerable<Guid> Ids, bool trackChanges);
        void DeleteCompany(Company company);
    }
}