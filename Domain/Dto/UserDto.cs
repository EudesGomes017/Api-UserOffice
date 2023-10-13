using Domain.Enums;
using Domain.Models;
using System.Text.Json.Serialization;

namespace Domain.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public bool? IsActive { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string FancyName { get; set; }
    public string? Documento { get; set; }
    public StatusUser Person { get; set; }
    [JsonIgnore]
    public virtual AddressRegister? AddressRegister { get; set; }
}

