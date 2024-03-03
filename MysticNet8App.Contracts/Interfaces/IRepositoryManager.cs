using MysticNet8App.Contracts.RepoInterfaces;

namespace MysticNet8App.Contracts.Interfaces;

public interface IRepositoryManager
{
    ICompanyRepository Company { get; }
    IEmployeeRepository Employee { get; }
    void Save();
}