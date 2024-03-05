using MysticNet8App.Contracts.Request;
using MysticNet8App.Contracts.Response;

namespace MysticAppNet8App.Application.Interfaces;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
    EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges);
    EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeRequestDto
        employeeForCreation, bool trackChanges);
}