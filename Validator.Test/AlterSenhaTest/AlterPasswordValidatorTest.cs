using Domain.Services.serviceUser.loggedInUser;
using Exceptions;
using FluentAssertions;
using Xunit;

namespace Validator.Test.AlterSenhaTest
{
    public class AlterPasswordValidatorTest
    {

        [Fact]
        public void Validate_Erro_PasswordEmpty()
        {
            var validator = new AlterPasswordValidator();

            var request = ChangeRequestBuilder.Builder();

            request.Passwordnew = string.Empty;

            var result = validator.Validate(request);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMenssagensErro.PASSWORD_BRANCO));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Validate_Erro_PasswordInvalid(int sizePassword)
        {
            var validator = new AlterPasswordValidator();

            var request = ChangeRequestBuilder.Builder(sizePassword);

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}

