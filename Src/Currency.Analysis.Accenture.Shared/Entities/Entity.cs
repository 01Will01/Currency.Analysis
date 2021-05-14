using System;

namespace Currency.Analysis.Accenture.Shared.Entities
{
    public class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
