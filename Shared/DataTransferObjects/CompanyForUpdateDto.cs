namespace Shared
{
        public record CompanyForUpdateDto(string Name, string Address, string Country, IEnumerable<EmployeeForCreationDto> Employees);
}