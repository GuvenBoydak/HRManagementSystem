namespace HrManagement.Domain.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
        IsDeleted = false;
    }

    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }
}