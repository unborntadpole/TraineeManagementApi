namespace TraineeManagementApi.Models;

using System.ComponentModel.DataAnnotations;

public class CreateTraineeRequest
{
    public long Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(100, ErrorMessage = "First name must be smaller than 100 characters")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(100, ErrorMessage = "Last name must be smaller than 100 characters")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Tech stack is required.")]
    public string TechStack { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [AllowedValues(["Active", "Inactive", "Completed"], ErrorMessage = "Enter a valid status")]
    public string Status { get; set; }

}
public class UpdateTraineeRequest
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? TechStack { get; set; }
    public string? Status { get; set; }
    public DateTime UpdatedDate { get; } = DateTime.UtcNow;
}
public class TraineeResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string TechStack { get; set; }
    public string Status { get; set; }
    
    public DateTime CreatedDate { get; set;}
    public DateTime? UpdatedDate { get; set; }

}
