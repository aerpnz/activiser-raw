using System;
using ExtensionMethods;

namespace activiser.WebService.CrmOutputGateway.CrmSdk
{
    public partial class Key
    {
        static public explicit operator Guid(Key value)
        {
            return value.Value;
        }

        public bool Equals(Key value) 
        {
            return this.Value == value.Value;
        }

        public bool Equals(Guid value)
        {
            return this.Value == value;
        }

        public bool Equals(string value)
        {
            Guid g;
            if (((string)value).IsGuid(out g))
                return this.Value == g;

            return false;
        }

        //public override bool Equals(object value)
        //{
        //    if (value is Guid)
        //        return this.Equals((Guid)value);

        //    if (value is string)
        //        return this.Equals((string)value);

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return this.Value.GetHashCode();
        //}
    }

    public partial class KeyProperty : Property
    {
        static public explicit operator Guid(KeyProperty value)
        {
            return value.Value.Value;
        }

        public bool Equals(KeyProperty value)
        {
            return this.Value == value.Value;
        }

        public bool Equals(Key value)
        {
            return this.Value == value;
        }

        public bool Equals(Guid value)
        {
            return this.Value.Equals(value);
        }

        //public override bool Equals(object value)
        //{
        //    return this.Value == value;
        //}

        //public override int GetHashCode()
        //{
        //    return this.Value.Value.GetHashCode();
        //}
    }

    public partial class UniqueIdentifier
    {
        static public explicit operator Guid(UniqueIdentifier value)
        {
            if (value.IsNull) return Guid.Empty;
            return value.Value;
        }

        public bool Equals(UniqueIdentifier value)
        {
            if (this.IsNull && value.IsNull) return true;
            if (this.IsNull || value.IsNull) return false;
            return this.Value == value.Value;
        }

        public bool Equals(string value)
        {
            Guid g;
            if (((string)value).IsGuid(out g))
                return this.Value == g;

            return false;
        }

        public bool Equals(Guid value)
        {
            if (this.IsNull) return false;
            return this.Value == value;
        }

        //public override bool Equals(object value)
        //{
        //    return this.Value == (Guid)value;
        //}

        //public override int GetHashCode()
        //{
        //    return this.IsNull ? Guid.Empty.GetHashCode() : this.Value.GetHashCode();
        //}
    }

    public partial class UniqueIdentifierProperty : Property
    {
        static public explicit operator Guid(UniqueIdentifierProperty value)
        {
            return (Guid)value.Value;
        }

        public bool Equals(UniqueIdentifier value)
        {
            return this.Value == value;
        }

        public bool Equals(Guid value)
        {
            if (this.Value.IsNull) return value == Guid.Empty;
            return this.Value.Value == value;
        }

        public bool Equals(string value)
        {
            if (string.IsNullOrEmpty(value) && this.Value.IsNull)
                return true;

            if (string.IsNullOrEmpty(value) || this.Value.IsNull)
                return false;

            Guid g;
            if (((string)value).IsGuid(out g))
                return this.Equals(g);

            return false;
        }

        //public override bool Equals(object value)
        //{
        //    if (this.Value.IsNull)
        //        return value == null;

        //    if (value is Guid)
        //        return this.Equals((Guid)value);

        //    if (value is string)
        //        return this.Equals((string)value);

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return this.Value.GetHashCode();
        //}

    }

    public abstract partial class CrmReference
    {
        static public explicit operator Guid(CrmReference value)
        {
            if (value.IsNull) return Guid.Empty;
            return (Guid)value.Value;
        }

        public bool Equals(CrmReference value)
        {
            if (this.IsNull && value.IsNull) return true;
            if (this.IsNull || value.IsNull) return false;
            return 
                this.Value == value.Value
                && this.name == value.name
                && this.type == value.type
                ;
        }

        public bool Equals(Guid value)
        {
            return !this.IsNull && (this.Value == value);
        }

        public bool Equals(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            Guid g;
            if (((string)value).IsGuid(out g))
                return this.Value == g;

            return false;
        }

        //public override bool Equals(object value)
        //{
        //    if (value is Guid)
        //        return this.Equals((Guid)value);

        //    if (value is string)
        //        return this.Equals((string)value);

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return this.IsNull?Guid.Empty.GetHashCode():this.Value.GetHashCode();
        //}
    }

    public partial class LookupProperty : Property
    {
        static public explicit operator Guid(LookupProperty value)
        {
            return (Guid)value.Value; // Lookup is a sub-class of CrmReference
        }

        public bool Equals(CrmReference value)
        {
            return (CrmReference)this.Value == value ;
        }

        public bool Equals(LookupProperty value)
        {
            return (CrmReference) this.Value == (CrmReference) value.Value;
        }

        public bool Equals(Guid value)
        {
            return !this.Value.IsNull && (this.Value.Value == value);
        }

        public bool Equals(string value)
        {
            if (string.IsNullOrEmpty(value) && this.Value.IsNull)
                return true;

            if (string.IsNullOrEmpty(value) || this.Value.IsNull)
                return false;

            Guid g;
            if (((string)value).IsGuid(out g))
                return this.Equals(g);

            return false;
        }

        //public override bool Equals(object value)
        //{
        //    CrmReference r = value as CrmReference;
        //    if (r != null)
        //        return (CrmReference) this.Value == r;

        //    if (value is Guid)
        //        return ((CrmReference)this.Value).Equals((Guid)value);

        //    if (value is string)
        //        return ((CrmReference)this.Value).Equals((string)value);

        //    if (this.Value.IsNull)
        //        return value == null;

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return ((CrmReference)this.Value).GetHashCode();
        //}

    }

    public partial class OwnerProperty : Property
    {
        static public explicit operator Guid(OwnerProperty value)
        {
            return (Guid)value.Value; // Lookup is a sub-class of CrmReference
        }

        public bool Equals(CrmReference value)
        {
            return (CrmReference)this.Value == value;
        }

        public bool Equals(OwnerProperty value)
        {
            return (CrmReference)this.Value == (CrmReference)value.Value;
        }

        public bool Equals(Guid value)
        {
            return !this.Value.IsNull && (this.Value.Value == value);
        }

        public bool Equals(string value)
        {
            if (string.IsNullOrEmpty(value) && this.Value.IsNull)
                return true;

            if (string.IsNullOrEmpty(value) || this.Value.IsNull)
                return false;

            Guid g;
            if (((string)value).IsGuid(out g))
                return this.Equals(g);

            return false;
        }

        //public override bool Equals(object value)
        //{
        //    CrmReference r = value as CrmReference;
        //    if (r != null)
        //        return (CrmReference)this.Value == r;

        //    if (value is Guid)
        //        return ((CrmReference)this.Value).Equals((Guid)value);

        //    if (value is string)
        //        return ((CrmReference)this.Value).Equals((string)value);

        //    if (this.Value.IsNull)
        //        return value == null;

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return ((CrmReference)this.Value).GetHashCode();
        //}
    }

    public partial class CustomerProperty : Property
    {
        static public explicit operator Guid(CustomerProperty value)
        {
            return (Guid)value.Value; // Lookup is a sub-class of CrmReference
        }

        public bool Equals(CrmReference value)
        {
            return (CrmReference)this.Value == value;
        }

        public bool Equals(CustomerProperty value)
        {
            return (CrmReference)this.Value == (CrmReference)value.Value;
        }

        public bool Equals(Guid value)
        {
            return !this.Value.IsNull && (this.Value.Value == value);
        }

        public bool Equals(string value)
        {
            if (string.IsNullOrEmpty(value) && this.Value.IsNull)
                return true;

            if (string.IsNullOrEmpty(value) || this.Value.IsNull)
                return false;

            Guid g;
            if (((string)value).IsGuid(out g))
                return this.Equals(g);

            return false;
        }

        //public override bool Equals(object value)
        //{
        //    CrmReference r = value as CrmReference;
        //    if (r != null)
        //        return (CrmReference)this.Value == r;

        //    if (value is Guid)
        //        return ((CrmReference)this.Value).Equals((Guid)value);

        //    if (value is string)
        //        return ((CrmReference)this.Value).Equals((string)value);

        //    if (this.Value.IsNull)
        //        return value == null;

        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return ((CrmReference)this.Value).GetHashCode();
        //}

    }
}