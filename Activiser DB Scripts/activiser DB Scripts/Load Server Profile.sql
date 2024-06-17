/*

If using SQL Query Analyzer (SQL 2000) or an edition of Microsoft SQL Server Management Studio (SQL 2005), then this script
will work as a template. Before running, press Control-Shift-M, and fill in the relevant fields.

*/

DECLARE @Now DATETIME
SET @Now = GETUTCDATE()
INSERT INTO [ServerProfile]([ItemType],[Activated],[SystemID],[Notes],[CreatedDateTime],[ModifiedDateTime]) 
	      SELECT 'L', 1, '<License Key, text, GCEQD3TBZ4ZCETGR>', '<Licensee Name, text, Activiser Trial>',  @Now, @Now 
	UNION SELECT 'S', 1, '<Short Message Email Address Template, email , {Number}@SMSProvider.com>', 'You have been assigned request {RequestNumber} for {ClientName}. Please synchronise your activiser™ client', @Now, @Now
	UNION SELECT 'M', 1, '<SMTP Server Port, number, 25>', '<SMTP Server, name or IP , localhost>', @Now, @Now
	UNION SELECT 'A', 1, '<Service Account, email , activiser@mydomain.local>', '<Administrator, email , activiser@mydomain.local>', @Now, @Now
	UNION SELECT 'J', 1, NULL, 'This is an automated message from {LicenseeName}.

As requested, please find the details of the work completed at {ClientSite.SiteAddress}
by our consultant {Consultant.Name}.

Started: {Job.StartTime}
Finished: {Job.FinishTime}

Job No: {Job.JobNumber}

Details/Equipment:
{Job.JobDetails}
{Job.Equipment}


If you have any questions about this work, please contact us at {AdminEmail} 

Kind Regards,

{LicenseeName}', @Now, @Now

GO
