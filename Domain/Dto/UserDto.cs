using Domain.Enums;
using Domain.Models;

namespace Domain.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string FancyName { get; set; }
    public bool UserDesativado { get; set; }
    public int CPF { get; set; }
    public int CNPJ { get; set; }
    public StatusUser Person { get; set; }
    public virtual AddressRegister AddressRegister { get; set; }
}

