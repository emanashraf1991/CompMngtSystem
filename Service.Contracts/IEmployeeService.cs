using Entities;
using Shared;
namespace Service.Contracts;
public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges);
    Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
    Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges);
    Task DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
    Task UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForCreation, bool compTrackChanges, bool empTrackChanges);
    Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
   Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);
    Task SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}
