﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace activiser.SchemaEditor.DataAccessLayer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="kinetics")]
	public partial class CandidateEntityDataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCandidateEntity(CandidateEntity instance);
    partial void UpdateCandidateEntity(CandidateEntity instance);
    partial void DeleteCandidateEntity(CandidateEntity instance);
    partial void InsertCandidateEntityAttribute(CandidateEntityAttribute instance);
    partial void UpdateCandidateEntityAttribute(CandidateEntityAttribute instance);
    partial void DeleteCandidateEntityAttribute(CandidateEntityAttribute instance);
    #endregion
		
		public CandidateEntityDataClassesDataContext() : 
				base(global::activiser.SchemaEditor.DataAccessLayer.Properties.Settings.Default.kineticsConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CandidateEntityDataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CandidateEntityDataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CandidateEntityDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CandidateEntityDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CandidateEntity> CandidateEntities
		{
			get
			{
				return this.GetTable<CandidateEntity>();
			}
		}
		
		public System.Data.Linq.Table<CandidateEntityAttribute> CandidateEntityAttributes
		{
			get
			{
				return this.GetTable<CandidateEntityAttribute>();
			}
		}
	}
	
	[Table(Name="metadata.CandidateEntity")]
	public partial class CandidateEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _SchemaName;
		
		private string _EntityName;
		
		private string _SqlObjectType;
		
		private EntitySet<CandidateEntityAttribute> _CandidateEntityAttributes;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSchemaNameChanging(string value);
    partial void OnSchemaNameChanged();
    partial void OnEntityNameChanging(string value);
    partial void OnEntityNameChanged();
    partial void OnSqlObjectTypeChanging(string value);
    partial void OnSqlObjectTypeChanged();
    #endregion
		
		public CandidateEntity()
		{
			this._CandidateEntityAttributes = new EntitySet<CandidateEntityAttribute>(new Action<CandidateEntityAttribute>(this.attach_CandidateEntityAttributes), new Action<CandidateEntityAttribute>(this.detach_CandidateEntityAttributes));
			OnCreated();
		}
		
		[Column(Storage="_SchemaName", DbType="NVarChar(128) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string SchemaName
		{
			get
			{
				return this._SchemaName;
			}
			set
			{
				if ((this._SchemaName != value))
				{
					this.OnSchemaNameChanging(value);
					this.SendPropertyChanging();
					this._SchemaName = value;
					this.SendPropertyChanged("SchemaName");
					this.OnSchemaNameChanged();
				}
			}
		}
		
		[Column(Storage="_EntityName", DbType="NVarChar(128) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string EntityName
		{
			get
			{
				return this._EntityName;
			}
			set
			{
				if ((this._EntityName != value))
				{
					this.OnEntityNameChanging(value);
					this.SendPropertyChanging();
					this._EntityName = value;
					this.SendPropertyChanged("EntityName");
					this.OnEntityNameChanged();
				}
			}
		}
		
		[Column(Storage="_SqlObjectType", DbType="Char(2) NOT NULL", CanBeNull=false)]
		public string SqlObjectType
		{
			get
			{
				return this._SqlObjectType;
			}
			set
			{
				if ((this._SqlObjectType != value))
				{
					this.OnSqlObjectTypeChanging(value);
					this.SendPropertyChanging();
					this._SqlObjectType = value;
					this.SendPropertyChanged("SqlObjectType");
					this.OnSqlObjectTypeChanged();
				}
			}
		}
		
		[Association(Name="CandidateEntity_CandidateEntityAttribute", Storage="_CandidateEntityAttributes", ThisKey="SchemaName,EntityName", OtherKey="SchemaName,EntityName")]
		public EntitySet<CandidateEntityAttribute> CandidateEntityAttributes
		{
			get
			{
				return this._CandidateEntityAttributes;
			}
			set
			{
				this._CandidateEntityAttributes.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_CandidateEntityAttributes(CandidateEntityAttribute entity)
		{
			this.SendPropertyChanging();
			entity.CandidateEntity = this;
		}
		
		private void detach_CandidateEntityAttributes(CandidateEntityAttribute entity)
		{
			this.SendPropertyChanging();
			entity.CandidateEntity = null;
		}
	}
	
	[Table(Name="metadata.CandidateEntityAttribute")]
	public partial class CandidateEntityAttribute : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _SchemaName;
		
		private string _EntityName;
		
		private string _AttributeName;
		
		private string _AttributeType;
		
		private System.Nullable<int> _AttributeIsPK;
		
		private System.Nullable<int> _AttributeOrderInPK;
		
		private bool _AttributeHoldsComputedValue;
		
		private System.Nullable<bool> _AttributeIsNullable;
		
		private EntityRef<CandidateEntity> _CandidateEntity;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSchemaNameChanging(string value);
    partial void OnSchemaNameChanged();
    partial void OnEntityNameChanging(string value);
    partial void OnEntityNameChanged();
    partial void OnAttributeNameChanging(string value);
    partial void OnAttributeNameChanged();
    partial void OnAttributeTypeChanging(string value);
    partial void OnAttributeTypeChanged();
    partial void OnAttributeIsPKChanging(System.Nullable<int> value);
    partial void OnAttributeIsPKChanged();
    partial void OnAttributeOrderInPKChanging(System.Nullable<int> value);
    partial void OnAttributeOrderInPKChanged();
    partial void OnAttributeHoldsComputedValueChanging(bool value);
    partial void OnAttributeHoldsComputedValueChanged();
    partial void OnAttributeIsNullableChanging(System.Nullable<bool> value);
    partial void OnAttributeIsNullableChanged();
    #endregion
		
		public CandidateEntityAttribute()
		{
			this._CandidateEntity = default(EntityRef<CandidateEntity>);
			OnCreated();
		}
		
		[Column(Storage="_SchemaName", DbType="NVarChar(128) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string SchemaName
		{
			get
			{
				return this._SchemaName;
			}
			set
			{
				if ((this._SchemaName != value))
				{
					if (this._CandidateEntity.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSchemaNameChanging(value);
					this.SendPropertyChanging();
					this._SchemaName = value;
					this.SendPropertyChanged("SchemaName");
					this.OnSchemaNameChanged();
				}
			}
		}
		
		[Column(Storage="_EntityName", DbType="NVarChar(128) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string EntityName
		{
			get
			{
				return this._EntityName;
			}
			set
			{
				if ((this._EntityName != value))
				{
					if (this._CandidateEntity.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnEntityNameChanging(value);
					this.SendPropertyChanging();
					this._EntityName = value;
					this.SendPropertyChanged("EntityName");
					this.OnEntityNameChanged();
				}
			}
		}
		
		[Column(Storage="_AttributeName", DbType="NVarChar(128)", IsPrimaryKey=true)]
		public string AttributeName
		{
			get
			{
				return this._AttributeName;
			}
			set
			{
				if ((this._AttributeName != value))
				{
					this.OnAttributeNameChanging(value);
					this.SendPropertyChanging();
					this._AttributeName = value;
					this.SendPropertyChanged("AttributeName");
					this.OnAttributeNameChanged();
				}
			}
		}
		
		[Column(Storage="_AttributeType", DbType="NVarChar(256)")]
		public string AttributeType
		{
			get
			{
				return this._AttributeType;
			}
			set
			{
				if ((this._AttributeType != value))
				{
					this.OnAttributeTypeChanging(value);
					this.SendPropertyChanging();
					this._AttributeType = value;
					this.SendPropertyChanged("AttributeType");
					this.OnAttributeTypeChanged();
				}
			}
		}
		
		[Column(Storage="_AttributeIsPK", DbType="Int")]
		public System.Nullable<int> AttributeIsPK
		{
			get
			{
				return this._AttributeIsPK;
			}
			set
			{
				if ((this._AttributeIsPK != value))
				{
					this.OnAttributeIsPKChanging(value);
					this.SendPropertyChanging();
					this._AttributeIsPK = value;
					this.SendPropertyChanged("AttributeIsPK");
					this.OnAttributeIsPKChanged();
				}
			}
		}
		
		[Column(Storage="_AttributeOrderInPK", DbType="Int")]
		public System.Nullable<int> AttributeOrderInPK
		{
			get
			{
				return this._AttributeOrderInPK;
			}
			set
			{
				if ((this._AttributeOrderInPK != value))
				{
					this.OnAttributeOrderInPKChanging(value);
					this.SendPropertyChanging();
					this._AttributeOrderInPK = value;
					this.SendPropertyChanged("AttributeOrderInPK");
					this.OnAttributeOrderInPKChanged();
				}
			}
		}
		
		[Column(Storage="_AttributeHoldsComputedValue", DbType="Bit NOT NULL")]
		public bool AttributeHoldsComputedValue
		{
			get
			{
				return this._AttributeHoldsComputedValue;
			}
			set
			{
				if ((this._AttributeHoldsComputedValue != value))
				{
					this.OnAttributeHoldsComputedValueChanging(value);
					this.SendPropertyChanging();
					this._AttributeHoldsComputedValue = value;
					this.SendPropertyChanged("AttributeHoldsComputedValue");
					this.OnAttributeHoldsComputedValueChanged();
				}
			}
		}
		
		[Column(Storage="_AttributeIsNullable", DbType="Bit")]
		public System.Nullable<bool> AttributeIsNullable
		{
			get
			{
				return this._AttributeIsNullable;
			}
			set
			{
				if ((this._AttributeIsNullable != value))
				{
					this.OnAttributeIsNullableChanging(value);
					this.SendPropertyChanging();
					this._AttributeIsNullable = value;
					this.SendPropertyChanged("AttributeIsNullable");
					this.OnAttributeIsNullableChanged();
				}
			}
		}
		
		[Association(Name="CandidateEntity_CandidateEntityAttribute", Storage="_CandidateEntity", ThisKey="SchemaName,EntityName", OtherKey="SchemaName,EntityName", IsForeignKey=true)]
		public CandidateEntity CandidateEntity
		{
			get
			{
				return this._CandidateEntity.Entity;
			}
			set
			{
				CandidateEntity previousValue = this._CandidateEntity.Entity;
				if (((previousValue != value) 
							|| (this._CandidateEntity.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CandidateEntity.Entity = null;
						previousValue.CandidateEntityAttributes.Remove(this);
					}
					this._CandidateEntity.Entity = value;
					if ((value != null))
					{
						value.CandidateEntityAttributes.Add(this);
						this._SchemaName = value.SchemaName;
						this._EntityName = value.EntityName;
					}
					else
					{
						this._SchemaName = default(string);
						this._EntityName = default(string);
					}
					this.SendPropertyChanged("CandidateEntity");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
