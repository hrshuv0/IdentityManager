using Infrastructure.Core.Enums;

namespace Infrastructure.Core.Entities;

public class BaseEntity<T> : IBaseEntity<T>
{
    public T Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
    public Status Status { get; set; }
}