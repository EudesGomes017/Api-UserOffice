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
        CreateMap<User, LoginUserDto>().ReverseMap();
        CreateMap<User, AlterPasswordUpDto>().ReverseMap();

        //CreateMap<AddressRegister, User>() // Mapeamento entre AddressRegister e User
        //.ForMember(dest => dest.AddressRegister, opt => opt.MapFrom(src => src.User))
        //// Mapeie outras propriedades conforme necessário
        //.ReverseMap(); // Se precisar de mapeamento reverso
    }
}

