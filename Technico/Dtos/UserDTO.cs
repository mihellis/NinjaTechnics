﻿using Technico.Models;

namespace Technico.Dtos;

public class UserRequestDTO
{
    public Guid Id { get; set; }
    public string VATNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public User.Type Role { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
public class UserResponseDTO
{
     public Guid Id { get; set; }
    public string VATNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public User.Type Role { get; set; }

    public List<PropertyDTO> Properties { get; set; } = new List<PropertyDTO>();
}
