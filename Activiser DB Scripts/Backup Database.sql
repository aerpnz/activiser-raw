DECLARE @fileName nvarchar(256)
SELECT @fileName = N'C:\Projects2008\Activiser3.5.0\Activiser DB Scripts\activiserDB-' 
	+ convert(nvarchar, getdate(), 112) + N'.BAK'

BACKUP DATABASE [activiser] TO 
DISK = @fileName
WITH INIT, NAME = N'activiser Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10

PRINT 'Backup to ' + @fileName + ' complete.'


SELECT @fileName = N'C:\Projects2008\Activiser3.5.0\Activiser DB Scripts\kineticsDB-' 
	+ convert(nvarchar, getdate(), 112) + N'.BAK'

BACKUP DATABASE kinetics TO 
DISK = @fileName
WITH INIT, NAME = N'kineticsDB Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10

PRINT 'Backup to ' + @fileName + ' complete.'
