using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        
        public Guid Id { get; init; }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is Entity entity && Equals(entity);
        }

        public bool Equals(Entity? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Entity? left, Entity? right) => Equals(left, right);
        public static bool operator !=(Entity? left, Entity? right) => !Equals(left, right);
    }
}
