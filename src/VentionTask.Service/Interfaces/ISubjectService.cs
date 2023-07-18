using Ventiontask.Service.DTOs;

namespace Ventiontask.Service.Interfaces;

public interface ISubjectService
{
    Task<SubjectResultDto> AddAsync(SubjectCreationDto dto);
    Task<SubjectResultDto> ModifyAsync(long id, SubjectCreationDto dto);
    Task<bool> RemoveAsync(long id);
    Task<SubjectResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<SubjectResultDto>> RetrieveAllAsync();
}
