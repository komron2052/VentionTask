using Ventiontask.Domain.Commons;

namespace Ventiontask.Domain.Entities;

public class GroupSubject : Auditable
{
    public long GroupId { get; set; }
    public Group Group { get; set; } = null!;
    public long SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}
