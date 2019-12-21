using System;

namespace ProductCategory
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
    }
}
