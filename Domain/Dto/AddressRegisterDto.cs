namespace Domain.Dto;

public class AddressRegisterDto
{
    public int Id { get; set; }
    public int Cep { get; set; }
    public string Patio { get; set; }
    public string Neighborhood { get; set; }
    public string Locality { get; set; }
    public string UF { get; set; }
    public int UserId { get; set; }
}

