using FluentValidation;


namespace PrivateSchool.Models.Custom_Validations
{
    public class StudentValidator: AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.FirstName)
                .NotEmpty()
                .WithMessage("Is required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters")
                .Matches("^[a-zA-Z_ ]*$")
                .WithMessage("Only letters please");
            
            RuleFor(s => s.LastName)
                .NotEmpty()
                .WithMessage("Is required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters")
                .Matches("^[a-zA-Z_ ]*$")
                .WithMessage("Only letters please");

            RuleFor(s => s.DateOfBirth)
                .NotEmpty()
                .WithMessage("Is required");

            RuleFor(s => s.TuitionFees)
                .NotEmpty()
                .WithMessage("Is required");
                
                


        }
       
    }
}