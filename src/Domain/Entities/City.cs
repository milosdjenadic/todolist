using System.ComponentModel.DataAnnotations;

namespace Assignment.Domain.Entities;
public class City : BaseAuditableEntity
{
    [StringLength(100)]
    public string Name { get; set; } = null!;

    public Country Country { get; set; } = null!;

    public int CountryId { get; set; }
}
