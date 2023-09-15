using Domain.Enums;

namespace Domain.Dto;

public class DepartmentDto
{
    public int Id { get; set; }
    public string Namedepartment { get; set; }
    public string Responsible { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public int UserId { get; set; }
    public StatusUser Qulification { get; set; }
}

