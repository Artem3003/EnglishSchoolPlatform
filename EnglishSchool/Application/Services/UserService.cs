using AutoMapper;
using System.Text;
using Application.Interfaces;
using Application.DTOs.User;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<UserDto?> GetByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);
        user.PasswordHash = HashPassword(createUserDto.Password);

        var createdUser = await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserDto> UpdateAsync(UpdateUserDto updateUserDto)
    {
        var existingUser = await _unitOfWork.Users.GetByIdAsync(updateUserDto.Id) ?? throw new ArgumentException("User not found");
        _mapper.Map(updateUserDto, existingUser);
        await _unitOfWork.Users.UpdateAsync(existingUser);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(existingUser);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }

        await _unitOfWork.Users.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    private static string HashPassword(string password)
    {
        var hashedBytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}
