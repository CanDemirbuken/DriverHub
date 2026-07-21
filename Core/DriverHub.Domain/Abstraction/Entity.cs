namespace DriverHub.Domain.Abstraction;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}