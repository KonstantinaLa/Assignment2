using FluentValidation;

namespace PrivateSchool.Models.Custom_Validations
{
    public class TrainerValidator:AbstractValidator<Trainer>
    {
        public TrainerValidator()
        {
            RuleFor(t => t.FirstName)
                .NotEmpty()
                .WithMessage("Is required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters")
                .Matches("^[a-zA-Z_ ]*$")
                .WithMessage("Only letters please");

            RuleFor(t => t.LastName)
                .NotEmpty()
                .WithMessage("Is required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters")
                .Matches("^[a-zA-Z_ ]*$")
                .WithMessage("Only letters please");

            RuleFor(t => t.Subject)
                .NotEmpty()
                .WithMessage("Is required")
                .Length(2, 20)
                .WithMessage("Length 2-20 characters")
                .Matches("^[a-zA-Z_ ]*$")
                .WithMessage("Only letters please");


        }
    }
}