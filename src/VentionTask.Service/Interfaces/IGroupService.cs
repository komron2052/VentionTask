using Ventiontask.Service.DTOs;

namespace Ventiontask.Service.Interfaces;

public interface IGroupService
{
    Task<GroupResultDto> AddAsync(GroupCreationDto dto);
    Task<GroupResultDto> ModifyAsync(long id, GroupCreationDto dto);
    Task<bool> RemoveAsync(long id);
    Task<GroupResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<GroupResultDto>> RetrieveAllAsync();
}
