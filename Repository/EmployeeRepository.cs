using Contracts;
using Entities;
namespace Repository;
public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {
    }
    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
                FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                                .OrderBy(e => e.Name).ToList();
    public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.Id.Equals(id) && e.CompanyId.Equals(companyId), trackChanges)
                            .SingleOrDefault();

    public void CreateEmployeeForCompany(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }
    public void DeleteEmployee(Employee employee) => Delete(employee);

}