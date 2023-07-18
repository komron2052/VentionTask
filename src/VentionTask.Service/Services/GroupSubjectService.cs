using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ventiontask.Data.IRepositories;
using Ventiontask.Domain.Entities;
using Ventiontask.Service.DTOs;
using Ventiontask.Service.Exceptions;
using Ventiontask.Service.Interfaces;

namespace Ventiontask.Service.Services;

public class GroupSubjectService : IGroupSubjectService
{
    private readonly IRepository<GroupSubject> gsRepository;
    private readonly IMapper mapper;

    public GroupSubjectService(IRepository<GroupSubject> gsRepository, IMapper mapper)
    {
        this.gsRepository = gsRepository;
        this.mapper = mapper;
    }

    public async Task<GroupSubjectResultDto> AddAsync(GroupSubjectCreationDto dto)
    {
        var mappedGroupSubject = this.mapper.Map<GroupSubject>(dto);
        mappedGroupSubject.CreatedAt = DateTime.UtcNow;
        var addedGroupSubject = await this.gsRepository.InsertAsync(mappedGroupSubject);

        await this.gsRepository.SaveAsync();

        return this.mapper.Map<GroupSubjectResultDto>(addedGroupSubject);
    }

    public async Task<GroupSubjectResultDto> ModifyAsync(long id, GroupSubjectCreationDto dto)
    {
        var groupSubject = await this.gsRepository.SelectAsync(gs => gs.Id == id);
        if (groupSubject == null)
            throw new CustomException(404, "Not found!");

        var modifiedGroupSubject = this.mapper.Map(dto, groupSubject);
        modifiedGroupSubject.UpdatedAt = DateTime.UtcNow;

        await this.gsRepository.SaveAsync();

        return this.mapper.Map<GroupSubjectResultDto>(modifiedGroupSubject);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var subject = await this.gsRepository.SelectAsync(gs => gs.Id == id);
        if (subject == null)
            throw new CustomException(404, "Not found!");

        await this.gsRepository.DeleteAsync(gs => gs.Id == id);

        await this.gsRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<GroupSubjectResultDto>> RetrieveAllAsync()
    {
        var groupSubject = await this.gsRepository.SelectAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<GroupSubjectResultDto>>(groupSubject);
    }

    public async Task<GroupSubjectResultDto> RetrieveByIdAsync(long id)
    {
        var groupSubject = await this.gsRepository.SelectAsync(gs => gs.Id == id);
        if (groupSubject == null)
            throw new CustomException(404, "Not found!");

        return this.mapper.Map<GroupSubjectResultDto>(groupSubject);
    }
}
