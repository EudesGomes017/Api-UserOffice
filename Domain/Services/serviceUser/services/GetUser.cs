using Domain.Interface.RepositoryDomain;
using Exceptions.ExceptionBase;
using Exceptions;
using Domain.Dto;
using Domain.Services.serviceUser.InterfaceUsersServices;
using AutoMapper;

namespace Domain.Services.serviceUser.services;

public class GetUser : IGetUserRepositoryDomainDto
{
    private readonly IGetUserRepositoryDomain _userRepositoryDomain;
    private readonly IMapper _mapper;
    public GetUser(IGetUserRepositoryDomain userRepositoryDomain, IMapper mapper)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
    }

    public async Task<UserDto[]> SearchAllUsersAsync()
    {
        UserDto[] user;

        try
        {
            var result = await _userRepositoryDomain.AllUsersAsync();
            user = _mapper.Map<UserDto[]>(result);

            foreach (var item in user)
            {
                item.Password = "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        if (user.Length > 0)
        {
            return user;
        }

        throw new ErroValidatorException(new List<string> { ResourceMenssagensErro.LISTA_VAZIA });
    }

    public async Task<UserDto> SearchUserIdAsync(int? id)
    {
        UserDto user;
        try
        {
            var result = await _userRepositoryDomain.UserByIdAsync(id);
            user = _mapper.Map<UserDto>(result);
        }

        catch (Exception)
        {

            throw new ErroValidatorException(new List<string> { ResourceMenssagensErro.BUSCA_ID_USER });

        }
        return user;
    }
}
