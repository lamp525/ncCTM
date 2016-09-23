using System;

namespace CTM.Core
{
    public abstract partial class BaseEntity
    {
        #region Properties

        public int Id { get; set; }

        //public string FieldA { get; set; }

        //public  string FieldB { get; set; }

        //public string FieldC { get; set; }

        #endregion Properties

        #region Utilities

        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        #endregion Utilities

        #region Methods

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        #endregion Methods
    }
}