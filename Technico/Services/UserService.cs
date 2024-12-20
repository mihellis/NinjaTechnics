using Technico.Models;
using Technico.Repositories;
using Technico.Dtos;

namespace Technico.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDTO?> CreateAsync(UserRequestDTO createDto)
    {
        var users = await _userRepository.GetAllAsync();

        bool VATexists = users.Any(u => u.VATNumber == createDto.VATNumber);
        if (VATexists)return null;

        bool emailExists = users.Any(u => u.Email == createDto.Email);
        if (emailExists) return null;

        var user = new User
        {
            VATNumber = createDto.VATNumber,
            Name = createDto.Name,
            Surname = createDto.Surname,
            Address = createDto.Address,
            PhoneNumber = createDto.PhoneNumber,
            Email = createDto.Email,
            Password = createDto.Password,
        };
        var result = await _userRepository.CreateAsync(user);

        return result == null ? null : new UserResponseDTO
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email
        };
    }


    public async Task<UserResponseDTO?> UpdateAsync(Guid id, UserRequestDTO user)
    {
        var existingUser = await _userRepository.GetAsync(id);
        if (existingUser == null) return null;

        // Update the fields
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.Surname = user.Surname;
        existingUser.Role = user.Role;
        existingUser.VATNumber = user.VATNumber;

        var updatedUser = await _userRepository.UpdateAsync(existingUser);

        return newUserDTO(updatedUser);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<UserResponseDTO?> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null) return null;

        return newUserDTO(user);
    }

    public async Task<UserResponseDTO?> ValidateUserByEmail(string email, string password)
    {
        var user = await _userRepository.GetAsyncByEmail(email);
        if (user == null) return null;

        if (password != user.Password)
        {
            return null;
        }

        UserResponseDTO userDTO = newUserDTO(user);
        return userDTO;
    }

    public async Task<List<UserResponseDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => newUserDTO(user)).ToList();
    }

    private static UserResponseDTO newUserDTO(User user)
    {
        return new UserResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            Address = user.Address,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber,
            VATNumber = user.VATNumber,
            Email = user.Email,
            Role = user.Role,
            Properties = user.Properties.Select(property => new PropertyDTO
            {
                OwnerID = user.Id,
                PropertyIDNumber = property.PropertyIDNumber,
                Address = property.Address,
                YearOfConstruction = property.YearOfConstruction
            }).ToList()
        };
    }
}
