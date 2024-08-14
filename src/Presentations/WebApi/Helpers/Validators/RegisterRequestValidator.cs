using FluentValidation;
using Models.DTOs.Account;

namespace WebApi.Helpers.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.FirstName).NotEmpty();
        RuleFor(r => r.LastName).NotEmpty();
        RuleFor(r => r.Email).NotEmpty().EmailAddress();
        RuleFor(r => r.Password).NotEmpty().MinimumLength(6);
        RuleFor(r => r.ConfirmPassword).NotEmpty().Equal(r => r.Password)
            .WithMessage("The password and confirmation password do not match.");
    }
}