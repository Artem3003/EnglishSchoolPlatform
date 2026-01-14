using System.ComponentModel.DataAnnotations;

namespace Payments.Domain.Entities.Common;

public abstract class BaseEntity<TId>
{
    [Key]
    public TId? Id { get; set; }
}
