SELECT     MetadataSchema.Entity.LogicalName AS EntityName, MetadataSchema.Attribute.Name AS AttributeName, 
                      MetadataSchema.AttributeTypes.Description AS AttributeType
FROM         MetadataSchema.Attribute INNER JOIN
                      MetadataSchema.Entity ON MetadataSchema.Attribute.EntityId = MetadataSchema.Entity.EntityId INNER JOIN
                      MetadataSchema.AttributeTypes ON MetadataSchema.Attribute.AttributeTypeId = MetadataSchema.AttributeTypes.AttributeTypeId
ORDER BY EntityName, AttributeName
