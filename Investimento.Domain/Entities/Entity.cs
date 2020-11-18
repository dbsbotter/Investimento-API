using System;

namespace Investimento.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public void SetCreatedAt()
        {
            CreatedAt = DateTime.Now;
        }

        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}