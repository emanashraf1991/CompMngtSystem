using Contracts;
using Entities;
namespace Repository;
public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {

    }
    public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
                                                    FindAll(trackChanges)
                                                    .OrderBy(c => c.Name)
                                                    .ToList();
    public Company GetCompany(Guid companyId, bool trackChanges) =>
       FindByCondition(c => c.Id.Equals(companyId), trackChanges)
       .SingleOrDefault();

    public void CreateCompany(Company company) => Create(company);

    public IEnumerable<Company> GetByIDs(IEnumerable<Guid> Ids, bool trackChanges) =>
        FindByCondition(c => Ids.Contains(c.Id), trackChanges).ToList();

    public void DeleteCompany(Company company) => Delete(company);
}