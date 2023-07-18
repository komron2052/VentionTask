using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ventiontask.Data.IRepositories;
using Ventiontask.Domain.Entities;
using Ventiontask.Service.DTOs;
using Ventiontask.Service.Exceptions;
using Ventiontask.Service.Interfaces;

namespace Ventiontask.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> userRepository;
    private readonly IMapper mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UserResultDto> AddAsync(UserCreationDto dto)
    {
        var user = await this.userRepository.SelectAsync(u => u.Phone == dto.Phone);
        if (user != null)
            throw new CustomException(409, "User already exists!");

        var mappedUser = this.mapper.Map<User>(dto);
        mappedUser.CreatedAt = DateTime.UtcNow;
        var addedUser = await this.userRepository.InsertAsync(mappedUser);

        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(addedUser);
    }

    public async Task<UserResultDto> ModifyAsync(long id, UserUpdateDto dto)
    {
        var user = await this.userRepository.SelectAsync(u => u.Id == id);
        if (user == null)
            throw new CustomException(404, "Not found!");

        var modifiedUser = this.mapper.Map(dto, user);
        modifiedUser.UpdatedAt = DateTime.UtcNow;

        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(modifiedUser);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await this.userRepository.SelectAsync(u => u.Id == id);
        if (user == null)
            throw new CustomException(404, "Not found!");

        await this.userRepository.DeleteAsync(u => u.Id == id);

        await this.userRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<UserResultDto>> RetrieveAllAsync()
    {
        var users = await this.userRepository.SelectAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<UserResultDto>>(users);  
    }

    public async Task<UserResultDto> RetrieveByIdAsync(long id)
    {
        var user = await this.userRepository.SelectAsync(u => u.Id == id);
        if (user == null)
            throw new CustomException(404, "Not found!");

        return this.mapper.Map<UserResultDto>(user);
    }
}
