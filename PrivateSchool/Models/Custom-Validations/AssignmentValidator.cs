using FluentValidation;

namespace PrivateSchool.Models.Custom_Validations
{
    public class AssignmentValidator:AbstractValidator<Assignment>
    {
        public AssignmentValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .WithMessage("Required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters");

            RuleFor(a => a.Description)
                .NotEmpty()
                .WithMessage("Required")
                .Length(2, 20)
                .Matches("^[a-zA-Z_ ]*$")
                .WithMessage("Length 2-20 characters");

            RuleFor(a => a.SubDate)
                .NotEmpty()
                .WithMessage("Required");

            RuleFor(a => a.OralMark)
                .NotEmpty()
                .WithMessage("Required");

            RuleFor(a => a.TotalMark)
                .NotEmpty()
                .WithMessage("Required");



        }



    }
}