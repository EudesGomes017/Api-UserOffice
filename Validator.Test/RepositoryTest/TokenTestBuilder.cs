using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Token;

namespace Validator.Test.RepositoryTest;

public class TokenTestBuilder
{
    public static TokenController TokenInstantiates()
    {
        return new TokenController(1000, "MFdOYHdhWWFFeisxNl5VKktKJjp6Li9MJjdPWHY9NU4tP3I6SXVGQsKjJDQzOG5LO8KjYg==");

    }
}

