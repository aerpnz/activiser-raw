SELECT     MetadataSchema.Entity.Name AS EntityName, StringMap.AttributeValue as StatusCode, 
                      MetadataSchema.AttributePicklistValue.State_Status_Value as StateCode, StringMap.Value as Description
FROM         MetadataSchema.AttributePicklistValue INNER JOIN
                      MetadataSchema.Attribute ON MetadataSchema.AttributePicklistValue.AttributeId = MetadataSchema.Attribute.AttributeId INNER JOIN
                      MetadataSchema.Entity ON MetadataSchema.Attribute.EntityId = MetadataSchema.Entity.EntityId INNER JOIN
                      StringMap ON MetadataSchema.Entity.ObjectTypeCode = StringMap.ObjectTypeCode AND 
                      MetadataSchema.Attribute.Name = StringMap.AttributeName AND MetadataSchema.AttributePicklistValue.Value = StringMap.AttributeValue
WHERE     (NOT (MetadataSchema.AttributePicklistValue.State_Status_Value IS NULL))  and AttributeName = 'statuscode'
ORDER BY EntityName, AttributeName, MetadataSchema.AttributePicklistValue.DisplayOrder