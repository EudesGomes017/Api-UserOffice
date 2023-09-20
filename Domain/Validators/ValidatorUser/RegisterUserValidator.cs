using Domain.Dto;
using FluentValidation;

namespace Domain.Validators.ValidatorUser;

public class RegisterUserValidator : AbstractValidator<UserDto>
{
	public RegisterUserValidator()
	{

	}
}

