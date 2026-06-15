namespace TraineeManagementApi.Services;

using FluentValidation;
using TraineeManagementApi.DTO;

public class CreateTraineeRequestValidator : AbstractValidator<CreateTraineeRequest>
{
    // private readonly string[] _validStatus = ["Active", "Inactive", "Completed"];

    enum ValidStatus
    {
        Active,
        Inactive,
        Completed
    }
    public CreateTraineeRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must be smaller than 50 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must be smaller than 50 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex).WithMessage("Invalid Email");
        
        RuleFor(x => x.TechStack)
            .NotEmpty().WithMessage("TechStack is required");
        
        RuleFor(x => x.Status)
            // .Must( status => _validStatus.Contains(status)).WithMessage("Invalid status")
            .Must( status => Enum.IsDefined(typeof(ValidStatus),status)).WithMessage("Invalid status")
            .NotEmpty().WithMessage("Status is required");
        
    }
}

public class UpdateTraineeRequestValidator : AbstractValidator<UpdateTraineeRequest>
{
    // private readonly string[] _validStatus = ["Active", "Inactive", "Completed"];

    enum ValidStatus
    {
        Active,
        Inactive,
        Completed
    }
    public UpdateTraineeRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must be smaller than 50 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must be smaller than 50 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex).WithMessage("Invalid Email");
        
        RuleFor(x => x.TechStack)
            .NotEmpty().WithMessage("TechStack is required");
        
        RuleFor(x => x.Status)
            // .Must( status => _validStatus.Contains(status)).WithMessage("Invalid status")
            .Must( status => Enum.IsDefined(typeof(ValidStatus),status)).WithMessage("Invalid status")
            .NotEmpty().WithMessage("Status is required");
        
    }
}