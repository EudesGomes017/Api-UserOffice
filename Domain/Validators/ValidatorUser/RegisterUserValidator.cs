using Domain.Dto;
using Domain.Validators.ValidatorDocument;
using Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Domain.Validators.ValidatorUser;

public class RegisterUserValidator : AbstractValidator<UserDto>
{
    public RegisterUserValidator()
    {
        RuleFor(n => n.Name).NotEmpty().WithMessage(ResourceMenssagensErro.ADICIAONAR_USER);
        RuleFor(e => e.Email).NotEmpty().WithMessage(ResourceMenssagensErro.BUSCA_ID_EMAIL);

        When(e => !string.IsNullOrWhiteSpace(e.Email), () =>
        {
            RuleFor(e => e.Email).EmailAddress().WithMessage(ResourceMenssagensErro.EMAIL_INVALIDO);
        });
        RuleFor(p => p.Password).NotEmpty().WithMessage(ResourceMenssagensErro.PASSWORD_BRANCO);

        When(p => !string.IsNullOrWhiteSpace(p.Password), () =>
        {
            RuleFor(p => p.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMenssagensErro.PASSWORD_CARACTER);
        });

        RuleFor(x => x.Documento)
            .NotEmpty()
            .WithMessage(ResourceMenssagensErro.PASSWORD_CARACTER);

        /// função de validação customizada, para a propriedade documento
        When(p => !string.IsNullOrWhiteSpace(p.Documento), () =>
        {
            RuleFor(x => x.Documento)
          .Must(ValidatorDocument)
          .WithMessage(ResourceMenssagensErro.DOCUMENTO_INVALIDO);
            //RuleFor(p => p.Documento).Custom((documento, contexto) =>
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
        var result = false;

        var validadorDcument = new DocumentValidator();
       
       
        string padraDcoumento = Regex.Replace(document, @"\D", ""); // \D corresponde a qualquer caractere que não seja um dígito


        if (padraDcoumento.Length == 11)
        {
            result = validadorDcument.validatorDocumentCpf(padraDcoumento);
        }
        
        if (padraDcoumento.Length == 14)
        {
            result = validadorDcument.validatorDocumentCnpj(padraDcoumento);
        }
        return result;
    }
}

