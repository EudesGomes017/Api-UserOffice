using AutoMapper;
using Domain.Dto;
using Domain.Integracao.Refit;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Validators.ValidatorUser;
using Exceptions;
using Exceptions.ExceptionBase;

namespace Domain.Services.serviceUser.services;

public class PostUser : IPostUser
{
    private readonly IGetUserRepositoryDomain _userRepositoryDomain; 
    private readonly IMapper _mapper;
    private readonly EncryptPassword _encryptPassword; 
    private readonly ISearchEamil _searchEamil;
    private readonly IVerifyDocument _verifyDocumento;
    private readonly IVerifyPassWord _verifyPassWord;

    public PostUser(IGetUserRepositoryDomain userRepositoryDomain, IMapper mapper, EncryptPassword encryptPassword,
         ISearchEamil searchEamil, IVerifyDocument verifyDocumento, IVerifyPassWord verifyPassWord)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
        _encryptPassword = encryptPassword;
        _verifyPassWord = verifyPassWord;
        _searchEamil = searchEamil;
        _verifyDocumento = verifyDocumento;
    }

    public async Task<User> AddUserAsync(UserDto user)
    {
        await validator(user);

        try
        {
            var Password = _encryptPassword.encrypt(user.Password);
            var result = _mapper.Map<User>(user);
            result.Password = Password;
            result.UpdateAt = DateTime.Now;
            _userRepositoryDomain.Adicionar(result);
            await _userRepositoryDomain.SalvarMudancasAsync();
            result.Password = "";

            return result;
        }
        catch (ErroValidatorException)
        {
            throw new ErroValidatorException(new List<string> { "erro Servidor" });
        }
    }

    private async Task validator(UserDto user)
    {

        var validator = new RegisterUserValidator();
        var result = validator.Validate(user);
        var existUserEmail = await _searchEamil.SearchrEmail(user.Email);
        var existDocument = await _verifyDocumento.SearchrDocument(user.Document);
        var existPassword = await _verifyPassWord.SearchrPasssword(user.Password);

        if (existUserEmail)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMenssagensErro.EMAIL_CADASTRADO));
        }

        if (existDocument)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("document", ResourceMenssagensErro.DOCUMENTO_EXISTE));
        }

        if (existDocument)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("password", ResourceMenssagensErro.PASSWORD_EXISTE));
        }

        if (!result.IsValid)
        {
            var messageErro = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErroValidatorException(messageErro);
        }
    }
}

