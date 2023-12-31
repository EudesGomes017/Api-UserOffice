﻿using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class Department
{
    public int Id { get; set; }
    public string Namedepartment { get; set; }
    public string Responsible { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public int UserId { get; set; }
    public User user { get; set; }
    public StatusUser Qulification { get; set; }

}

