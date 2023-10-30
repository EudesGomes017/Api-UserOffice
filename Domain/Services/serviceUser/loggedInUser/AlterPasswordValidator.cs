using Domain.Dto;
using FluentValidation;

namespace Domain.Services.serviceUser.loggedInUser
{
    public class AlterPasswordValidator : AbstractValidator<AlterPasswordUpDto>
    {
        public AlterPasswordValidator()
        {
            RuleFor(e => e.SenhaNova).SetValidator(new PasswordValidate());
        }
    }


}

