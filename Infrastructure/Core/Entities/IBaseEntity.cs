using Infrastructure.Core.Enums;

namespace Infrastructure.Core.Entities;

public interface IBaseEntity<T>
{
    T Id { get; set; }
    string? Name { get; set; }

    DateTime CreationDate { get; set; }
    DateTime ModificationDate { get; set; }
    
    Guid CreatedBy { get; set; }
    Guid ModifiedBy { get; set; }

    Status Status { get; set; }

}