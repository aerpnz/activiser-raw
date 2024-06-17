using System;
using System.Collections.Generic;
using System.Reflection;
using activiser.WebService.OutputGateway;

namespace activiser.WebService.CrmOutputGateway.CrmSdk
{
	public partial class DynamicEntity
	{
		public void Remove(string propertyName)
		{
			this.Indexer.RemoveProperty(propertyName);
		}

		public bool Contains(string propertyName)
		{
			Property property;
			return this.Indexer.TryGetProperty(propertyName, out property);
		}

        public override string ToString()
        {
            return Serialization.Serialize<DynamicEntity>(this);
        }

        [System.Xml.Serialization.XmlIgnore()]
		public Property this[string propertyName]
		{
			get
			{
				Property property;
				if (Indexer.TryGetProperty(propertyName, out property))
				{
                    return property; // PropertyFactory.GetPropertyValue(property);
				}

				throw new ArgumentException(string.Format("Property '{0}' is not found.", propertyName), "propertyName");
			}

			set
			{
				Property property;
				if (Indexer.TryGetProperty(propertyName, out property))
				{
					PropertyFactory.SetPropertyValue(property, value);
				}
				else
				{
					Indexer.AddNewProperty(propertyName, value);
				}
			}
		}

        [System.Xml.Serialization.XmlIgnore()]
		private PropertyIndexer Indexer
		{
			get
			{
				if (_propertyIndexer == null)
				{
					_propertyIndexer = new PropertyIndexer(this);
				}

				return _propertyIndexer;
			}
		}

		[NonSerialized]
		private PropertyIndexer _propertyIndexer;

		public static class PropertyFactory
		{
			public static Property CreateInstance(string propertyName, object value)
			{
				Property p = null;
				if (value is CrmBoolean)
				{
					p = new CrmBooleanProperty();
				}
				else if (value is CrmDateTime)
				{
					p = new CrmDateTimeProperty();
				}
				else if (value is CrmDecimal)
				{
					p = new CrmDecimalProperty();
				}
				else if (value is CrmFloat)
				{
					p = new CrmFloatProperty();
				}
				else if (value is CrmMoney)
				{
					p = new CrmMoneyProperty();
				}
				else if (value is CrmNumber)
				{
					p = new CrmNumberProperty();
				}
				else if (value is Customer)
				{
					p = new CustomerProperty();
				}
				else if (value is DynamicEntity[])
				{
					p = new DynamicEntityArrayProperty();
				}
				else if (value is EntityNameReference)
				{
					p = new EntityNameReferenceProperty();
				}
				else if (value is Key)
				{
					p = new KeyProperty();
				}
				else if (value is Lookup)
				{
					p = new LookupProperty();
				}
				else if (value is Owner)
				{
					p = new OwnerProperty();
				}
				else if (value is Picklist)
				{
					p = new PicklistProperty();
				}
				else if (value is Status)
				{
					p = new StatusProperty();
				}
				else if (value is string)
				{
					if (propertyName == "statecode")
					{
						p = new StateProperty();
					}
					else
					{
						p = new StringProperty();
					}
				}
				else if (value is UniqueIdentifier)
				{
					p = new UniqueIdentifierProperty();
				}
				else
				{
                    throw new PropertyTypeNotSupportedException(value);
				}

				p.Name = propertyName;
				SetPropertyValue(p, value);

				return p;
			}

            public static object GetPropertyValue(Property property)
            {
                PropertyInfo p = property.GetType().GetProperty("Value");

                object result = p.GetValue(property, null);
                // System.Diagnostics.Debug.WriteLine(result.ToString());
                return result;
            }

			public static void SetPropertyValue(Property property, object value)
			{
				PropertyInfo p = property.GetType().GetProperty("Value");

				p.SetValue(property, value, null);
			}
		}

		private sealed class PropertyIndexer
		{
			public PropertyIndexer(DynamicEntity entity)
			{
				_entity = entity;
				_nameToIndex = new Dictionary<string, int>();
			}

			public bool TryGetProperty(string propertyName, out Property property)
			{
				int index;
                if (_nameToIndex.TryGetValue(propertyName, out index) && index < _entity.Properties.Length)
				{
					property = _entity.Properties[index];

					if (property.Name == propertyName)
					{
						return true;
					}
				}

				ReIndex();
				if (_nameToIndex.TryGetValue(propertyName, out index))
				{
					property = _entity.Properties[index];

					return true;
				}

				property = null;
				return false;
			}

            public Property AddNewProperty(Property property)
            {
                if (_entity.Properties == null)
                {
                    _entity.Properties = new Property[1];
                }
                else
                {
                    Array.Resize<Property>(ref _entity.propertiesField, _entity.Properties.Length + 1);
                }

                _entity.propertiesField[_entity.propertiesField.Length - 1] = property;

                _nameToIndex.Add(property.Name, _entity.propertiesField.Length - 1);

                return property;
            }

			public Property AddNewProperty(string propertyName, object value)
			{
				Property property = PropertyFactory.CreateInstance(propertyName, value);

				if (_entity.Properties == null)
				{
					_entity.Properties = new Property[1];
				}
				else
				{
					Array.Resize<Property>(ref _entity.propertiesField, _entity.Properties.Length + 1);
				}

				_entity.propertiesField[_entity.propertiesField.Length - 1] = property;

				_nameToIndex.Add(propertyName, _entity.propertiesField.Length - 1);

				return property;
			}

			public void RemoveProperty(string propertyName)
			{
				Property property;
				int index;
				if (_nameToIndex.TryGetValue(propertyName, out index))
				{
					if (index < _entity.Properties.Length)
					{
						property = _entity.Properties[index];

						if (property.Name == propertyName)
						{
							this.RemovePropertyAt(index);
							return;
						}
					}
				}

				ReIndex();
				if (_nameToIndex.TryGetValue(propertyName, out index))
				{
					this.RemovePropertyAt(index);
				}
			}

			private void RemovePropertyAt(int index)
			{
				// Move the last element into the index of the item removed.
				if (index != _entity.Properties.Length - 1)
				{
					_entity.Properties[index] = _entity.Properties[_entity.Properties.Length - 1];
				}

				// Resize the array one smaller.
				Array.Resize<Property>(ref _entity.propertiesField, _entity.Properties.Length - 1);
			}

			private void ReIndex()
			{
				if (_entity.propertiesField != null)
				{
					_nameToIndex = new Dictionary<string, int>(_entity.propertiesField.Length);

					for (int i = 0; i < _entity.propertiesField.Length; i++)
					{
						Property property = _entity.propertiesField[i];

						_nameToIndex.Add(property.Name, i);
					}
				}
				else
				{
					_nameToIndex = new Dictionary<string, int>();
				}
			}

			private DynamicEntity _entity;
			private Dictionary<string, int> _nameToIndex;
		}

        [global::System.Serializable]
        public class PropertyTypeNotSupportedException : Exception
        {
            //
            // For guidelines regarding the creation of new exception types, see
            //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
            // and
            //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
            //

            public PropertyTypeNotSupportedException() { }
            public PropertyTypeNotSupportedException(string message) : base(message) { }
            public PropertyTypeNotSupportedException(string message, Exception inner) : base(message, inner) { }
            protected PropertyTypeNotSupportedException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }

            public PropertyTypeNotSupportedException(object value)
                : base("Property type is not supported.")
            {
                _type = value.GetType();
            }

            private System.Type _type;
            public System.Type ObjectType { get { return _type; } }
        }
    }
}
