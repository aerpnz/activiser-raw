SELECT     MetadataSchema.Entity.Name AS EntityName, MetadataSchema.Attribute.Name AS AttributeName, 
                      MetadataSchema.AttributePicklistValue.State_Status_Value, MetadataSchema.AttributePicklistValue.DisplayOrder, 
                      MetadataSchema.Entity.ObjectTypeCode, StringMap.AttributeValue, StringMap.Value
FROM         MetadataSchema.AttributePicklistValue INNER JOIN
                      MetadataSchema.Attribute ON MetadataSchema.AttributePicklistValue.AttributeId = MetadataSchema.Attribute.AttributeId INNER JOIN
                      MetadataSchema.Entity ON MetadataSchema.Attribute.EntityId = MetadataSchema.Entity.EntityId INNER JOIN
                      StringMap ON MetadataSchema.Entity.ObjectTypeCode = StringMap.ObjectTypeCode AND 
                      MetadataSchema.Attribute.Name = StringMap.AttributeName AND MetadataSchema.AttributePicklistValue.Value = StringMap.AttributeValue
WHERE     (NOT (MetadataSchema.AttributePicklistValue.State_Status_Value IS NULL))
ORDER BY EntityName, AttributeName