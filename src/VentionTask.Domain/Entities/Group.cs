using Ventiontask.Domain.Commons;

namespace Ventiontask.Domain.Entities;

public class Group : Auditable
{
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
    public ICollection<GroupSubject> GroupSubjects { get; }

}
