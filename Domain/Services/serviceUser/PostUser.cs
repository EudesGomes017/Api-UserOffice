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

namespace Domain.Services.serviceUser;

public class PostUser : IPostUser
{

    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly ISearchEamil _searchEamil;

    private readonly IMapper _mapper;

    private readonly EncryptPassword _encryptPassword;
    private readonly TokenController _tokenController;

    public PostUser(IUserRepositoryDomain userRepositoryDomain, IMapper mapper, EncryptPassword encryptPassword, 
        TokenController tokenController, ISearchEamil searchEamil)
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
           
            await _userRepositoryDomain.SalvarMudancasAsync();
            
                return new ReplyJsonRegisteredUser
                {
                    Token = token
                };

        }
        catch (ErroValidatorException)
        {

            throw new ErroValidatorException(new List<string> { "erro Servidor" });
        }
    }

    private async Task validator(UserDto user)
    {

        var validator = new RegisterUserValidator();

       
        var resultado = validator.Validate(user);

        var existUserEmail = await _searchEamil.ExisteUserEmail(user.Email);


        if (existUserEmail)
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

