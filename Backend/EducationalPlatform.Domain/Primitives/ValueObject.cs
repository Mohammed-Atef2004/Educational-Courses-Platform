using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
      
        public abstract IEnumerable<object> GetAtomicValues();

        public bool Equals(ValueObject? other)
        {
            return other is not null && ValuesAreEqual(other);
        }

        public override bool Equals(object? obj)
        {
            return obj is ValueObject other && ValuesAreEqual(other);
        }

        private bool ValuesAreEqual(ValueObject other)
        {
            return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Aggregate(default(int), (hash, value) => hash ^ (value?.GetHashCode() ?? 0));
        }

        public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);
        public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);
    }
}
