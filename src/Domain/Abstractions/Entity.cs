namespace Domain.Abstractions;

public abstract class Entity : IEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.Now;
}
