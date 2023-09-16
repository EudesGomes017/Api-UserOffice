namespace Domain.Models;

    public class AddressRegister
    {
    public int Id { get; set; }
    public string Cep { get; set; }
    public string Patio { get; set; }
    public string Neighborhood { get; set; }
    public string Locality { get; set; }
    public string UF { get; set; } 
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}

