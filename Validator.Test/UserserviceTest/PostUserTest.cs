using Domain.Services.serviceUser;
using Exceptions;
using Exceptions.ExceptionBase;
using FluentAssertions;
using Moq;
using Validator.Test.MapperBuilderTest;
using Validator.Test.RepositoryTest;
using Validator.Test.UtilsTeste;
using Xunit;

namespace Validator.Test.UserserviceTest;

public class PostUserTest
{
    [Fact]
    public async Task Validator_Sucess()
    {
        var requisicao = RequestUserBuilder.Build();

        var createPost = CreatePost(requisicao.Email);

        Func<Task> acao = async () => { await createPost.AddUserAsync(requisicao); };

        await acao.Should().ThrowAsync<ErroValidatorException>()
                  .Where(erroException => erroException.MesssageError.Count == 1 && erroException.MesssageError.Contains(ResourceMenssagensErro.EMAIL_CADASTRADO));
        
    }

    private PostUser CreatePost(string email = "")
    {
        var repository = UserRepositoryDomainBuilderTest.UserInstantiates().build();
        var mapper = MapperTest.InstantiatesMapper();
        var encryptPassword = EncryptPasswordTestBuilder.Instantiates();
        var token = TokenTestBuilder.TokenInstantiates();
        var repositoryReadOnly = UserReadOnlyRepositoryBuilder.UserInstantiates().ExisteUserEmail(email).build();
         
        return new PostUser(repository, mapper, encryptPassword, token, repositoryReadOnly);
    }
}

