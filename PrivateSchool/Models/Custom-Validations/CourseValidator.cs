using FluentValidation;

namespace PrivateSchool.Models.Custom_Validations
{
    public class CourseValidator:AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .WithMessage("Is required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters");

            RuleFor(c => c.Stream)
                .NotEmpty()
                .WithMessage("Is required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters")
                .Matches("^[a-zA-Z_ ]*$")
                .WithMessage("Only letters");
            
            RuleFor(c => c.Type)
                .NotEmpty()
                .WithMessage("Is required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters")
                .Matches("^[a-zA-Z_ ]*$")
                .WithMessage("Only letters");

            RuleFor(c => c.StartDate)
                .NotEmpty()
                .WithMessage("Is required");

            RuleFor(c => c.EndDate)
                .NotEmpty()
                .WithMessage("Is required");


        }

        
    }
}