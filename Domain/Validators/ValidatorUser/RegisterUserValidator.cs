using Domain.Dto;
using Domain.Services.serviceUser.loggedInUser;
using Domain.Validators.StrategyDocument;
using Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Domain.Validators.ValidatorUser;

public class RegisterUserValidator : AbstractValidator<UserDto>
{

    public RegisterUserValidator()
    {
        RuleFor(n => n.Name).NotEmpty().WithMessage(ResourceMenssagensErro.NAME_USER);
        RuleFor(e => e.Email).NotEmpty().WithMessage(ResourceMenssagensErro.EMAIL_VAZIO);

        When(e => !string.IsNullOrWhiteSpace(e.Email), () =>
        {
            RuleFor(e => e.Email).EmailAddress().WithMessage(ResourceMenssagensErro.EMAIL_INVALIDO);
        });

       // RuleFor(e => e.Password).SetValidator(new PasswordValidate());
        RuleFor(p => p.Password).NotEmpty().WithMessage(ResourceMenssagensErro.PASSWORD_BRANCO);

        When(p => !string.IsNullOrWhiteSpace(p.Password), () =>
        {
            RuleFor(p => p.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMenssagensErro.PASSWORD_CARACTER);
        });

        RuleFor(x => x.Document)
            .NotEmpty()
            .WithMessage(ResourceMenssagensErro.DOCUMENTO_INVALIDO);

        /// função de validação customizada, para a propriedade documento
        When(p => !string.IsNullOrWhiteSpace(p.Document), () =>
        {
            RuleFor(x => x.Document)
          .Must(ValidatorDocument)
          .WithMessage(ResourceMenssagensErro.DOCUMENTO_INVALIDO);
            //RuleFor(p => p.Document).Custom((documento, contexto) =>
            //{

            //    var IsMatch = Regex.IsMatch(documento, @"\D");
            //    if (IsMatch)
            //    {
            //        contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(documento), ResourceMenssagensErro.DOCUMENTO_INVALIDO));
            //    }
            //});

        });
    }

    private bool ValidatorDocument(string document)
    {
        string padraDcoumento = Regex.Replace(document, @"\D", ""); // \D corresponde a qualquer caractere que não seja um dígito

        IStrategyValidatoDocument  validateDocument;      

        if (padraDcoumento.Length == 11)
        {
            validateDocument = new StrategyValidatorCpf();
        }

        else  if (padraDcoumento.Length == 14)
        {
            validateDocument = new StrategyValidatorCnpj();
        }
            
        else return false;

        return validateDocument.IsValidator(padraDcoumento);
    }
}

