﻿using AutoMapper;
using Domain.Dto;
using Domain.Interface.RepositoryDomain;
using Domain.Services.serviceUser.Criptorgrafia;
using Domain.Token;
using Exceptions;
using Exceptions.ExceptionBase;

namespace Domain.Services.serviceUser.AuthUser;

public class LoginUser : ILoginUser
{

    private readonly IUserRepositoryDomain _userRepositoryDomain;
    private readonly IMapper _mapper;
    private readonly EncryptPassword _encryptPassword;
    private readonly TokenController _tokenController;

    public LoginUser(IUserRepositoryDomain userRepositoryDomain, IMapper mapper, EncryptPassword encryptPassword, TokenController tokenController)
    {
        _userRepositoryDomain = userRepositoryDomain;
        _mapper = mapper;
        _encryptPassword = encryptPassword;
        _tokenController = tokenController;
    }

    public async Task<Object> UserByEmailAsync(LoginUserDto userLogin)
    {
        var token = "";

        try
        {
            var result = await _userRepositoryDomain.UserByEmailAsync(userLogin.Email);


            if (result == null)
            {
                throw new LoginInvalideException();
            }

            var Password = _encryptPassword.encrypt(userLogin.Password);

            if (Password == result.Password)
            {
                token = _tokenController.GerarToken(result);
            }

            return new { Id = result.Id, Name = result.Name, Email = result.Email, Token = token };
        }

        catch (ErroValidatorException ex)
        {
            throw ex; 
        }

       
    }
}
