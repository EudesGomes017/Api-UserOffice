using Exceptions;
using FluentValidation;

namespace Domain.Services.serviceUser.loggedInUser;

public class PasswordValidate : AbstractValidator<string>
{
    public PasswordValidate()
    {
        RuleFor(p => p).NotEmpty().WithMessage(ResourceMenssagensErro.PASSWORD_VAZIO);

        When(password => !string.IsNullOrWhiteSpace(password), () =>
        {
            RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMenssagensErro.PASSWORD_CARACTER);
        });

    }
}
