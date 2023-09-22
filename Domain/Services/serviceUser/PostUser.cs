using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Shared;
using Domain.Token;
using Domain.Validators.ValidatorUser;
using Exceptions;
using Exceptions.ExceptionBase;
using FluentValidation;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace Domain.Services.serviceUser;

public class PostUser : IPostUser
{

    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly ISearchEamil _searchEamil;

    private readonly IMapper _mapper;

    private readonly EncryptPassword _encryptPassword;
    private readonly TokenController _tokenController;

    public PostUser(IUserRepositoryDomain userRepositoryDomain, IMapper mapper, ISearchEamil searchEamil, EncryptPassword encryptPassword, TokenController tokenController)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _searchEamil = searchEamil;

        _encryptPassword = encryptPassword;
        _mapper = mapper;

        _tokenController = tokenController;

    }

    public async Task<ReplyJsonRegisteredUser> AddUserAsync(UserDto user)
    {

        await validator(user);

        try
        {
            user.Password = _encryptPassword.encrypt(user.Password); // senha criptografada
            var result = _mapper.Map<User>(user);
            result.UpdateAt = DateTime.Now;
            _userRepositoryDomain.Adicionar(result);

            var token = _tokenController.GerarToken(result.Email);

            if (await _userRepositoryDomain.SalvarMudancasAsync())
            {
                return new ReplyJsonRegisteredUser
                {
                    Token = token
                };
            }

            throw new SistemaTaskException();

        }
        catch (SistemaTaskException)
        {

            throw new SistemaTaskException();
        }

    }

    private async Task validator(UserDto user)
    {

        var validator = new RegisterUserValidator();

       
        var resultado = validator.Validate(user);

        var existUserEmail = await _searchEamil.SearchEamil(user.Email);


        if (existUserEmail != null)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMenssagensErro.EMAIL_CADASTRADO));
        }

        if (!resultado.IsValid)
        {
            var messageErro = resultado.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErroValidatorException(messageErro);
        }
    }
}

