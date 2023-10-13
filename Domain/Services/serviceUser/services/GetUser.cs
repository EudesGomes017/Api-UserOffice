using Domain.Interface.RepositoryDomain;
using Exceptions.ExceptionBase;
using Exceptions;
using Domain.Dto;
using Domain.Services.serviceUser.InterfaceUsersServices;
using AutoMapper;

namespace Domain.Services.serviceUser.services;

public class GetUser : IGetUser
{
    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly IMapper _mapper;
    public GetUser(IUserRepositoryDomain userRepositoryDomain, IMapper mapper)
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
