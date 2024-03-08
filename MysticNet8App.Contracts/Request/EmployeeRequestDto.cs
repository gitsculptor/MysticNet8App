using System.ComponentModel.DataAnnotations;

namespace MysticNet8App.Contracts.Request;

public record EmployeeRequestDto(
    [Required(ErrorMessage = "Company name is required.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    string Name,
        
    [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters.")]
    string Position,
    
    [Required]
    
    int Age
    );