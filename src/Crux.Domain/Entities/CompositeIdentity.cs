namespace Crux.Domain.Entities
{
    public class CompositeIdentity<TId, TSubId>
        where TId : struct
        where TSubId : struct
    {
        protected CompositeIdentity() { }

        public CompositeIdentity(TId id, TSubId subId)
        {
            ID = id;
            SubID = subId;
        }

        public TId ID { get; set; }
        public TSubId SubID { get; set; }

        #region Equality Override
        protected bool Equals(CompositeIdentity<TId, TSubId> other)
        {
            return SubID.Equals(other.SubID) && ID.Equals(other.ID);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((CompositeIdentity<TId, TSubId>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (SubID.GetHashCode() * 397) ^ ID.GetHashCode();
            }
        }
        #endregion
    }
}
