INSERT INTO [activiser].[dbo].[RequestStatus]
           ([RequestStatusID]
           ,[Description]
           ,[DisplayOrder]
           ,[ColourRed]
           ,[ColourGreen]
           ,[ColourBlue]
           ,[IsClientStatus]
           ,[IsClientMenuItem]
           ,[IsReasonRequired]
           ,[IsNewStatus]
           ,[IsInProgressStatus]
           ,[IsCompleteStatus]
           ,[IsCancelledStatus]
           ,[CreatedDateTime]
           ,[ModifiedDateTime])
		SELECT 1, 'New', 10, 128, 0, 0, 1, 1, 0, 1, 0, 0, 0, getutcdate(), getutcdate()
UNION	SELECT 2, 'In Progress', 20, 0, 128, 0, 1, 1, 0, 0, 1, 0, 0, getutcdate(), getutcdate()
UNION	SELECT 3, 'Complete', 30, 0, 0, 128, 1, 1, 0, 0, 0, 1, 0, getutcdate(), getutcdate()
UNION	SELECT 99, 'Cancelled', 40, 64, 64, 64, 1, 1, 0, 0, 0, 0, 1, getutcdate(), getutcdate()
