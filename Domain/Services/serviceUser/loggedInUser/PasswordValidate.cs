using Exceptions;
using FluentValidation;

namespace Domain.Services.serviceUser.loggedInUser;

public class PasswordValidate : AbstractValidator<string>
{
    public PasswordValidate()
    {
        RuleFor(password => password).NotEmpty().WithMessage(ResourceMenssagensErro.PASSWORD_BRANCO);

        When(password => !string.IsNullOrWhiteSpace(password), () =>
        {
            RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMenssagensErro.PASSWORD_CARACTER);
        });

    }
}
