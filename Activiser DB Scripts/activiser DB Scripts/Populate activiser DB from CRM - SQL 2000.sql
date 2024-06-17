/*

If using SQL Query Analyzer (SQL 2000) or an edition of Microsoft SQL Server Management Studio (SQL 2005), then this script
will work as a template. Before running, press Control-Shift-M, and fill in the relevant fields.


This script will probably not work out-of-the-box in your environment. For example, the client site status is (coincidentally) 
populated by default in activiser in a manner that is compatible with CRM, so it is not usually necessary to run that part of
the script.

Additionally, activiser requires the use of unique client names, which CRM doesn't. This script will not work correctly unless 
these are dealt with, either by excluding the duplicates, or changing the source data.

the DELETE commands are commented out by default, but can be usefull during initial use of the script.

*/

--
-- Clear tables
--
-- Comment out the following section if you want to clear your activiser database.
--

-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[Job]
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[Request]
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[RequestStatus] 
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[ClientSite]
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[ClientSiteStatus]
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[Consultant] WHERE ConsultantUID <> '00000000-0000-0000-0000-000000000000'
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[InterestedConsultant]
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[GatewayDuplicatedTask]
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[GatewayReturnValue]
-- DELETE FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.[GatewayTask]
-- GO
--


--
-- Populate ClientSite Status; for Microsoft CRM DB, this is either Active or Inactive, which coincidentally is the same as the
-- defaults for activiser, so this part is probably not necessary.
--
INSERT INTO <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.ClientSiteStatus (ClientSiteStatusID, Description, IsActive)
SELECT     AttributeValue, Value, CASE WHEN [Value] LIKE '%Inactive%' THEN 0 ELSE 1 END AS IsActive
FROM         <CRM DB, sysname, >.<CRM DB owner, sysname, dbo>.StringMap
WHERE     (ObjectTypeCode = 1) AND (AttributeName = 'statecode')
GO

--
-- Populate Request Status; for Microsoft CRM DB, this is the 'incidentstagecode' attribute of the 'incident' (case) entity.
-- 
-- it is populated from the CRM 'StringMap' table
--
INSERT INTO <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.RequestStatus
                      (RequestStatusID, Description, DisplayOrder, ColourRed, ColourGreen, ColourBlue, IsClientStatus, IsClientMenuItem, IsReasonRequired, IsNewStatus, 
                      IsInProgressStatus, IsCompleteStatus, IsCancelledStatus, CreatedDateTime, ModifiedDateTime)
SELECT     AttributeValue AS RequestStatusID, Value AS Description, DisplayOrder, 0 AS ColourRed, 0 AS ColourGreen, 255 AS ColourBlue, 1 AS IsClientStatus, 
                      1 AS IsClientMenuItem, 0 AS IsReasonRequired, CASE WHEN [Value] LIKE '%New%' THEN 1 ELSE 0 END AS IsNewStatus, 
                      CASE WHEN [Value] LIKE '%Progress%' THEN 1 ELSE 0 END AS IsInProgressStatus, 
                      CASE WHEN [Value] LIKE '%Complete%' THEN 1 ELSE 0 END AS IsCompleteStatus, 
                      CASE WHEN [Value] LIKE '%Cancelled%' THEN 1 ELSE 0 END AS IsCancelledStatus, GetUTCDate() AS CreatedDateTime, GetUTCDate() 
                      AS ModifiedDateTime
FROM         <CRM DB, sysname, >.<CRM DB owner, sysname, dbo>.StringMap
WHERE     (ObjectTypeCode = 112) AND (AttributeName = 'incidentstagecode')
GO

-- 
-- Add Consultants; for CRM DB, these are the 'system users'
--
INSERT INTO <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Consultant
				(ConsultantUID, Name, ConsultantID, MinSyncTime, MobilePhone, MobileAlert, EmailAddress, IsActiviserUser, Employee, Engineer, Management, Administration, 
				ModifiedDateTime, CreatedDateTime)
SELECT		SystemUserId, FullName, null, 0 AS MinSyncTime, MobilePhone, 
				CASE WHEN [MobileAlertEMail] IS NULL THEN 0 ELSE 1 END AS MobileAlert, InternalEMailAddress, 1 AS IsActiviserUser, 1 AS Employee, 1 AS Engineer, 1 AS Management, 
				1 AS Administration, CreatedOn, ModifiedOn
FROM        <CRM DB, sysname, >.<CRM DB owner, sysname, dbo>.SystemUserBase
WHERE		(DomainName <> '') AND 
			(SystemUserId NOT IN (SELECT DISTINCT ConsultantUID FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Consultant))
GO

-- 
-- Add ClientSites; for CRM DB, these are 'Accounts'
--
DECLARE @ROWSADDED int
SELECT @ROWSADDED=1

WHILE @ROWSADDED=1
BEGIN
INSERT INTO <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.ClientSite
                      (ClientSiteUID, ClientSiteNumber, SiteName, ClientSiteStatusID, SiteAddress, SiteContactEmail, SiteNotes, Contact, ContactPhone1, 
                      ContactPhone2, CreatedDateTime, ModifiedDateTime)
SELECT     TOP 1 AccountId, AccountNumber, Name, StateCode, Address1_Name + CHAR(13) + CHAR(10) 
                      + Address1_Line1 + CHAR(13) + CHAR(10) + Address1_Line2 + CHAR(13) + CHAR(10) + Address1_Line3 + CHAR(13) + CHAR(10) 
                      + Address1_City AS SiteAddress, EMailAddress1 AS SiteContactEmail, Description AS SiteNotes, PrimaryContactIdName AS Contact, 
                      Telephone1 AS ContactPhone1, Telephone2 AS ContactPhone2, CreatedOn, ModifiedOn
FROM         <CRM DB, sysname, >.<CRM DB owner, sysname, dbo>.Account
WHERE     (AccountId NOT IN (SELECT DISTINCT ClientSiteUID FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.ClientSite)) AND
		  (Name NOT IN (SELECT DISTINCT SiteName FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.ClientSite))

SELECT @ROWSADDED=@@ROWCOUNT
END
GO

-- 
-- Add Requests; for CRM DB, these are 'Cases', but the underlying Database entity is an 'incident'
--

INSERT INTO <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Request
				(RequestUID, RequestNumber, ClientSiteUID, AssignedToUID, Contact, LongDescription, ShortDescription, RequestStatusID, 
				NextActionDate, CompletedDate, ModifiedDateTime, CreatedDateTime)
SELECT		IncidentId, TicketNumber, AccountId, OwnerId, ContactIdName, Description, Title, 
				IncidentStageCode, FollowupBy, NULL AS CompletedDate, ModifiedOn, CreatedOn
FROM        <CRM DB, sysname, >.<CRM DB owner, sysname, dbo>.Incident
WHERE		(CustomerIdType = 1) AND 
			(AccountId IN (SELECT DISTINCT ClientSiteUID FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.ClientSite)) AND 
			(OwnerId IN (SELECT DISTINCT ConsultantUID FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Consultant)) AND 
			(IncidentId NOT IN (SELECT DISTINCT RequestUID FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Request))
GO

-- 
-- Add Jobs; for CRM DB, these are the 'Service Activities' (with an underlying database entity of 'serviceappointment'),
--		'Appointments' (underlying entity appointment) or 'Tasks' (underlying entity of 'Task').
--
-- Note, populating the Job table is not strictly necessary, unless you specifically want historical information getting to your PDAs.
--
INSERT INTO <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Job
				(JobUID, RequestUID, ClientSiteUID, ConsultantUID, JobDate, StartTime, FinishTime, JobDetails, EmailStatus, flag, jobstatusid, CreatedDateTime, ModifiedDateTime)
SELECT		ActivityId, RegardingObjectId, NULL AS ClientSiteUID, OwnerId, <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.ToLocalTime(ActualStart) 
				AS JobDate, ActualStart, ActualEnd, Description, 0 AS EmailStatus, 3 as flag, 3 as jobstatusid, CreatedOn, ModifiedOn
FROM		<CRM DB, sysname, >.<CRM DB owner, sysname, dbo>.<CRM Job Entity, sysname, ServiceAppointment|Appointment|Task>
WHERE		(RegardingObjectTypeCode = 112) AND 
			(OwnerIdType = 8) AND 
			(RegardingObjectId IN (SELECT RequestUID FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Request)) AND 
			(ActivityId NOT IN (SELECT JobUID FROM <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Job))
GO

UPDATE    <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Job
SET       ClientSiteUID = <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Request.ClientSiteUID
FROM      <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Job INNER JOIN
                <activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Request ON 
				<activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Job.RequestUID = 
				<activiser DB, sysname, activiser>.<activiser DB owner, sysname, dbo>.Request.RequestUID
GO
