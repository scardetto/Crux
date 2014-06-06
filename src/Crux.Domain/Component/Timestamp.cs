using System;
using Crux.Core.Extensions;

namespace Crux.Domain.Component
{
    public class Timestamp : IEquatable<Timestamp>
    {
        public virtual DateTime DateCreated { get; private set; }
        public virtual DateTime DateUpdated { get; private set; }

        public static Timestamp Now()
        {
            var now = DateTime.Now.ToTheSecond();
            return new Timestamp(now, now);
        }

        protected Timestamp() { }

        private Timestamp(DateTime dateCreated, DateTime dateUpdated)
        {
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }

        public virtual Timestamp Touch()
        {
            return new Timestamp(DateCreated, DateTime.Now.ToTheSecond());
        }

        #region Override Equality
        public bool Equals(Timestamp other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DateCreated.Equals(other.DateCreated) && DateUpdated.Equals(other.DateUpdated);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Timestamp)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (DateCreated.GetHashCode() * 397) ^ DateUpdated.GetHashCode();
            }
        }

        public static bool operator ==(Timestamp left, Timestamp right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Timestamp left, Timestamp right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
