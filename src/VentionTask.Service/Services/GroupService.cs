using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ventiontask.Data.IRepositories;
using Ventiontask.Domain.Entities;
using Ventiontask.Service.DTOs;
using Ventiontask.Service.Exceptions;
using Ventiontask.Service.Interfaces;

namespace Ventiontask.Service.Services;

public class GroupService : IGroupService
{
    private readonly IRepository<Group> groupRepository;
    private readonly IMapper mapper;

    public GroupService(IRepository<Group> groupRepository, IMapper mapper)
    {
        this.groupRepository = groupRepository;
        this.mapper = mapper;
    }

    public async Task<GroupResultDto> AddAsync(GroupCreationDto dto)
    {
        var group = await this.groupRepository.SelectAsync(g => g.Name.ToLower()
            == dto.Name.ToLower());
        if (group != null)
            throw new CustomException(409, "Group already exists!");

        var mappedGroup = this.mapper.Map<Group>(dto);
        mappedGroup.CreatedAt = DateTime.UtcNow;
        var addedGroup = await this.groupRepository.InsertAsync(mappedGroup);

        await this.groupRepository.SaveAsync();

        return this.mapper.Map<GroupResultDto>(addedGroup);
    }

    public async Task<GroupResultDto> ModifyAsync(long id, GroupCreationDto dto)
    {
        var group = await this.groupRepository.SelectAsync(g => g.Id == id);
        if (group == null)
            throw new CustomException(404, "Not found!");

        var modifiedGroup = this.mapper.Map(dto, group);
        modifiedGroup.UpdatedAt = DateTime.UtcNow;

        await this.groupRepository.SaveAsync();

        return this.mapper.Map<GroupResultDto>(modifiedGroup);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var group = await this.groupRepository.SelectAsync(g => g.Id == id);
        if (group == null)
            throw new CustomException(404, "Not found!");

        await this.groupRepository.DeleteAsync(g => g.Id == id);

        await this.groupRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<GroupResultDto>> RetrieveAllAsync()
    {
        var group = await this.groupRepository.SelectAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<GroupResultDto>>(group);
    }

    public async Task<GroupResultDto> RetrieveByIdAsync(long id)
    {
        var group = await this.groupRepository.SelectAsync(g => g.Id == id);
        if (group == null)
            throw new CustomException(404, "Not found!");

        return this.mapper.Map<GroupResultDto>(group);
    }
}
