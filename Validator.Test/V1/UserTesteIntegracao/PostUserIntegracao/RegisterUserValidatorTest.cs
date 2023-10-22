using Domain.Validators.StrategyDocument;
using Domain.Validators.ValidatorUser;
using Exceptions;
using FluentAssertions;
using Validator.Test.UtilsTeste;
using Xunit;

namespace Validator.Test.V1.UserTesteIntegracao.PostUserIntegracao
{
    public class RegisterUserValidatorTest
    {

        [Fact]
        public void Validator_Erro_NameVazioEmpty()
        {

            var validator = new RegisterUserValidator();
            var request = RequestUserBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMenssagensErro.NAME_USER));
        }

        [Fact]
        public void Validator_Erro_EmailVazioEmpty()
        {

            var validator = new RegisterUserValidator();
            var request = RequestUserBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMenssagensErro.EMAIL_VAZIO));
        }

        [Fact]
        public void Validator_Erro_PasswordVazioEmpty()
        {
            var validator = new RegisterUserValidator();
            var request = RequestUserBuilder.Build();
            request.Password = string.Empty;

            var result = validator.Validate(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMenssagensErro.PASSWORD_BRANCO));
        }

        [Fact]
        public void Validator_Erro_DocumentInvalid()
        {

            var validator = new RegisterUserValidator();
            var request = RequestUserBuilder.Build();
            request.Documento = "10000000000";

            var result = validator.Validate(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMenssagensErro.DOCUMENTO_INVALIDO));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Validator_Erro_PasswordInvalid(int tamanhoSenha)
        {

            var validator = new RegisterUserValidator();
            var request = RequestUserBuilder.Build(tamanhoSenha);

            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
    }
}