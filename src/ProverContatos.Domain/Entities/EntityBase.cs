namespace ProverContatos.Domain.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public EntityBase()
    {
        Id = Guid.NewGuid();
    }
}