namespace Crux.Domain.Entities
{
    public class DomainEntity<T>
    {
        public virtual T ID { get; set; }

        #region Override Equality
        /// <summary>
        /// Indicates whether the current DomainEntity is equal to another DomainEntity.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">A DomainEntity to compare with this object.</param>
        public virtual bool Equals(DomainEntity<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var otherIsTransient = Equals(other.ID, default(T));
            var thisIsTransient = Equals(ID, default(T));

            if (otherIsTransient && thisIsTransient)
                return ReferenceEquals(this, other);

            return other.ID.Equals(ID);
        }

        /// <summary>
        /// Determines whether the specified DomainEntity is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        /// true if the specified DomainEntity is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is DomainEntity<T>) return Equals((DomainEntity<T>)obj);
            if (obj.GetType() != GetType()) return false;

            return obj.Equals(this);
        }

        /// <summary>
        /// Serves as a hash function for a DomainEntity. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Equals(ID, default(T))
                ? base.GetHashCode()
                : ID.GetHashCode();
        }

        public static bool operator ==(DomainEntity<T> left, DomainEntity<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DomainEntity<T> left, DomainEntity<T> right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
