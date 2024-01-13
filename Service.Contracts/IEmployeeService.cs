using System.Dynamic;
using Shared;
namespace Service.Contracts;
public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
    EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges);
    EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges);
    void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);

    void UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForCreation, bool compTrackChanges, bool empTrackChanges);

}
