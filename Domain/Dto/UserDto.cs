using Domain.Enums;

namespace Domain.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? FancyName { get; set; }
    public string? Document { get; set; }
    public bool? IsActive { get; set; }
    public string cep { get; set; }
    public string logradouro { get; set; }
    public string complemento { get; set; }
    public string bairro { get; set; }
    public string localidade { get; set; }
    public string uf { get; set; }
    public string numero_da_casa { get; set; }
    public StatusUser Person { get; set; }
    public DateTime CreatedAt { get; set; }
    
}