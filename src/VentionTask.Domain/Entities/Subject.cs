using Ventiontask.Domain.Commons;

namespace Ventiontask.Domain.Entities;

public class Subject : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<GroupSubject> GroupSubjects { get; }
}
