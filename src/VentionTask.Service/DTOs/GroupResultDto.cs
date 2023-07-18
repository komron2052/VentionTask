using Ventiontask.Domain.Entities;

namespace Ventiontask.Service.DTOs;

public class GroupResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}
