// =====================================================================
//  File:		businessentitypartialtypes.cs
//  Summary:	Defines core types used with the SDK.
// =====================================================================
//
//  This file is part of the Microsoft CRM V4 SDK Code Samples.
//
//  Copyright (C) 2006 Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//
// =====================================================================
namespace activiser.WebService.CrmOutputGateway.CrmSdk
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Reflection;

	public partial class CrmBoolean
	{
		public CrmBoolean() { }

		public CrmBoolean(Boolean value)
		{
			this.Value = value;
		}
		
		public static CrmBoolean Null
		{
			get
			{
				CrmBoolean value = new CrmBoolean();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            CrmBoolean other = obj as CrmBoolean;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}

	public partial class CrmDateTime
	{
		public CrmDateTime() { }

		public CrmDateTime(string value)
		{
			this.Value = value;
		}

        public CrmDateTime(string value, string date, string time)
        {
            this.Value = value;
            this.date = date;
            this.time = time;
        }
        
        public DateTime UserTime
		{
			get
			{
				if (this.Value == null)
				{
					throw new InvalidOperationException("CrmDateTime must first be initialized.");
				}
				
				string userPart = this.Value.Substring(0, this.Value.LastIndexOf("-"));
				
				return DateTime.Parse(userPart, CultureInfo.InvariantCulture);
			}
		}
		
		public DateTime UniversalTime
		{
			get
			{
				if (this.Value == null)
				{
					throw new InvalidOperationException("CrmDateTime must first be initialized.");
				}
				
				return DateTime.Parse(this.Value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
			}
		}
		
		public static DateTime MinValue
		{
			get { return _minDateTime; }
		}

		public static DateTime MaxValue
		{
			get { return _maxDateTime; }
		}

		public static CrmDateTime FromUser(DateTime userTime)
		{
			return new CrmDateTime(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:s}", userTime));
		}

		public static CrmDateTime FromUniversal(DateTime universalTime)
		{
			return new CrmDateTime(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:s}Z", universalTime));
		}
		
		public static CrmDateTime Now
		{
			get 
			{
				return CrmDateTime.FromUniversal(DateTime.UtcNow);
			}
		}

        public static CrmDateTime Null
        {
            get
			{
                CrmDateTime value = new CrmDateTime();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}                            
        }

		private static readonly DateTime _minDateTime = new System.DateTime(1900, 1, 1);
		private static readonly DateTime _maxDateTime = new System.DateTime(9999, 12, 30, 23, 59, 59);
	}

	public partial class CrmDecimal
	{
		public CrmDecimal() { }

		public CrmDecimal(Decimal value)
		{
			this.Value = value;
		}

		public static CrmDecimal Null
		{
			get
			{
				CrmDecimal value = new CrmDecimal();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            CrmDecimal other = obj as CrmDecimal;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}

	public partial class CrmFloat
	{
		public CrmFloat() { }

		public CrmFloat(Single value)
		{
			this.Value = value;
		}

		public static CrmFloat Null
		{
			get
			{
				CrmFloat value = new CrmFloat();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            CrmFloat other = obj as CrmFloat;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}

	public partial class CrmMoney
	{
		public CrmMoney() { }

		public CrmMoney(Decimal value)
		{
			this.Value = value;
		}

		public static CrmMoney Null
		{
			get
			{
				CrmMoney value = new CrmMoney();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            CrmMoney other = obj as CrmMoney;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}

	public partial class CrmNumber
	{
		public CrmNumber() { }

		public CrmNumber(Int32 value)
		{
			this.Value = value;
		}

		public static CrmNumber Null
		{
			get
			{
				CrmNumber value = new CrmNumber();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            CrmNumber other = obj as CrmNumber;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}

	public partial class Customer
	{
		public Customer() { }

		public Customer(String type, Guid value)
		{
			this.type = type;
			this.Value = value;
		}

		public static Customer Null
		{
			get
			{
				Customer value = new Customer();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}
	}

	public partial class EntityNameReference
	{
		public EntityNameReference() { }

		public EntityNameReference(String value)
		{
			this.Value = value;
		}

        public override bool Equals(object obj)
        {
            EntityNameReference other = obj as EntityNameReference;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static EntityNameReference Null
        {         
            get
			{
                EntityNameReference value = new EntityNameReference();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}                                    
        }
	}

	public partial class Key
	{
		public Key() { }

		public Key(Guid value)
		{
			this.Value = value;
		}

        public override bool Equals(object obj)
        {
            Key other = obj as Key;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        // TODO: Need to define IsNull and IsNullSpecified.
        public static Key Null
        {
            get
            {
                return new Key(Guid.Empty);
            }
        }
	}

	public partial class Lookup
	{
		public Lookup() { }

		public Lookup(String type, Guid value)
		{
			this.type = type;
			this.Value = value;
		}

		public static Lookup Null
		{
			get
			{
				Lookup value = new Lookup();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            Lookup other = obj as Lookup;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}

	public partial class Owner
	{
		public Owner() { }

		public Owner(String type, Guid value)
		{
			this.type = type;
			this.Value = value;
		}

		public static Owner Null
		{
			get
			{
				Owner value = new Owner();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            Owner other = obj as Owner;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}

	public partial class Picklist
	{
		public Picklist() { }

		public Picklist(Int32 value)
		{
			this.Value = value;
		}

		public static Picklist Null
		{
			get
			{
				Picklist value = new Picklist();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            Picklist other = obj as Picklist;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}

	public partial class Status
	{
		public Status() { }

		public Status(Int32 value)
		{
			this.Value = value;
		}

		public static Status Null
		{
			get
			{
				Status value = new Status();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            Status other = obj as Status;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

	public partial class UniqueIdentifier
	{
		public UniqueIdentifier() { }

		public UniqueIdentifier(Guid value)
		{
			this.Value = value;
		}

		public static UniqueIdentifier Null
		{
			get
			{
				UniqueIdentifier value = new UniqueIdentifier();
				value.IsNull = true;
				value.IsNullSpecified = true;
				return value;
			}
		}

        public override bool Equals(object obj)
        {
            UniqueIdentifier other = obj as UniqueIdentifier;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
	}
}
