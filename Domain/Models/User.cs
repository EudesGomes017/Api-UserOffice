using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string FancyName { get; set; }
    public bool UserDesativado { get; set; }
    public int CPF { get; set; }
    public int CNPJ { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public StatusUser Person { get; set; }
    public virtual AddressRegister AddressRegister { get; set; }
    public virtual ICollection<Department?> Departments { get; set; }

}

