

using MysticAppNet8App.Application.Interfaces;

namespace MysticNet8App.Contracts.Interfaces;

public interface IServiceManager
{
    ICompanyService CompanyService { get; }
    
    IEmployeeService EmployeeService { get;}
}