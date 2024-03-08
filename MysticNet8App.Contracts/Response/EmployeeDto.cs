namespace MysticNet8App.Contracts.Response;

public record EmployeeDto(
    Guid Id,
    string? Name,
    string? Position,
    Guid CompanyId,
    int Age
    
    );