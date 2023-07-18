using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ventiontask.Data.IRepositories;
using Ventiontask.Domain.Entities;
using Ventiontask.Service.DTOs;
using Ventiontask.Service.Exceptions;
using Ventiontask.Service.Interfaces;

namespace Ventiontask.Service.Services;

public class SubjectService : ISubjectService
{
    private readonly IRepository<Subject> subjectRepository;
    private readonly IMapper mapper;
    public SubjectService(IRepository<Subject> subjectRepository, IMapper mapper)
    {
        this.subjectRepository = subjectRepository;
        this.mapper = mapper;
    }

    public async Task<SubjectResultDto> AddAsync(SubjectCreationDto dto)
    {
        var subject = await this.subjectRepository.SelectAsync(s => s.Name.ToLower()
            == dto.Name.ToLower());
        if (subject != null)
            throw new CustomException(409, "Subject already exists!");

        var mappedSubject = this.mapper.Map<Subject>(dto);
        mappedSubject.CreatedAt = DateTime.UtcNow;
        var addedSubject = await this.subjectRepository.InsertAsync(mappedSubject);

        await this.subjectRepository.SaveAsync();

        return this.mapper.Map<SubjectResultDto>(addedSubject);
    }

    public async Task<SubjectResultDto> ModifyAsync(long id, SubjectCreationDto dto)
    {
        var subject = await this.subjectRepository.SelectAsync(s => s.Id == id);
        if (subject == null)
            throw new CustomException(404, "Not found!");

        var modifiedSubject = this.mapper.Map(dto, subject);
        modifiedSubject.UpdatedAt = DateTime.UtcNow;

        await this.subjectRepository.SaveAsync();

        return this.mapper.Map<SubjectResultDto>(modifiedSubject);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var subject = await this.subjectRepository.SelectAsync(s => s.Id == id);
        if (subject == null)
            throw new CustomException(404, "Not found!");

        await this.subjectRepository.DeleteAsync(s => s.Id == id);

        await this.subjectRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<SubjectResultDto>> RetrieveAllAsync()
    {
        var subject = await this.subjectRepository.SelectAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<SubjectResultDto>>(subject);
    }

    public async Task<SubjectResultDto> RetrieveByIdAsync(long id)
    {
        var subject = await this.subjectRepository.SelectAsync(s => s.Id == id);
        if (subject == null)
            throw new CustomException(404, "Not found!");

        return this.mapper.Map<SubjectResultDto>(subject);
    }
}
