using AutoMapper;
using Domain.Dto;
using Domain.Models;

namespace Domain.Helps;

public class ApiSistemaProfile : Profile
{
    public ApiSistemaProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<AddressRegister, AddressRegisterDto>().ReverseMap();
        CreateMap<User, LoginUserDto>().ReverseMap();
        CreateMap<User, AlterPasswordUpDto>().ReverseMap();
    }
}

