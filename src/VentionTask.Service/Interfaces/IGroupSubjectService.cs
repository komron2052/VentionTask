using Ventiontask.Service.DTOs;

namespace Ventiontask.Service.Interfaces;

public interface IGroupSubjectService
{
    Task<GroupSubjectResultDto> AddAsync(GroupSubjectCreationDto dto);
    Task<GroupSubjectResultDto> ModifyAsync(long id, GroupSubjectCreationDto dto);
    Task<bool> RemoveAsync(long id);
    Task<GroupSubjectResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<GroupSubjectResultDto>> RetrieveAllAsync();
}
