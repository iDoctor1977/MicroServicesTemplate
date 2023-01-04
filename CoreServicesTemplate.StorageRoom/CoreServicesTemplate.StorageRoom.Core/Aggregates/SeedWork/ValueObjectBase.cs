using System.Collections.Generic;
using System.Linq;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork
{
    public abstract class ValueObjectBase : IAggModel
    {
        protected static bool EqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            return !(EqualOperator(left, right));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObjectBase)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public ValueObjectBase GetCopy()
        {
            return MemberwiseClone() as ValueObjectBase;
        }

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
