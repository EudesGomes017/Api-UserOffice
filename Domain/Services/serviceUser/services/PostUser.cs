using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Models;
using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Services.serviceUser.InterfaceUsersServices;
using Domain.Services.serviceUser.services.SharedUser;
using Domain.Validators.ValidatorUser;
using Exceptions;
using Exceptions.ExceptionBase;

namespace Domain.Services.serviceUser.services;

public class PostUser : IPostUser
{
    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly IMapper _mapper;
    private readonly EncryptPassword _encryptPassword;
    private readonly IVerificaPassWord _verificaPassWord;
    private readonly ISearchEamil _searchEamil;
    private readonly IVerificarDocumento _verificarDocumento;

    public PostUser(IUserRepositoryDomain userRepositoryDomain, IMapper mapper, EncryptPassword encryptPassword,
        IVerificaPassWord verificaPassWord, ISearchEamil searchEamil, IVerificarDocumento verificarDocumento)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
        _encryptPassword = encryptPassword;
        _verificaPassWord = verificaPassWord;
        _searchEamil = searchEamil;
        _verificarDocumento = verificarDocumento;
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
        var resultado = validator.Validate(user);
        var existUserEmail = await _searchEamil.SearchrEmail(user.Email);
        var existDocument = await _verificarDocumento.SearchrDocument(user.Documento);
        var existPassword = await _verificaPassWord.SearchrDocument(user.Password);

        if (existUserEmail)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMenssagensErro.EMAIL_CADASTRADO));
        }

        if (existDocument)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("document", ResourceMenssagensErro.DOCUMENTO_EXISTE));
        }

        if (existDocument)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("password", ResourceMenssagensErro.PASSWORD_EXISTE));
        }

        if (!resultado.IsValid)
        {
            var messageErro = resultado.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErroValidatorException(messageErro);
        }
    }
}

