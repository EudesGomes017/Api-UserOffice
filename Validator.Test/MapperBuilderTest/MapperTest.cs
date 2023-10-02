using AutoMapper;
using Domain.Helps;

namespace Validator.Test.MapperBuilderTest;

public class MapperTest
{
    public static IMapper InstantiatesMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ApiSistemaProfile>();
        });

        return configuration.CreateMapper();
    }
}

