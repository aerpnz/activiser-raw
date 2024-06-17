REM @ECHO OFF
SET BaseVersion=4.0.0
SET ActiviserVersion=Activiser%BaseVersion%

for /f "usebackq" %%v in (`C:\Projects\activiser\BuildSupport\GetBuildNumber.exe 2004-04-01`) do @set BuildNumber=%%v
@REM ECHO %BuildNumber%

for /f "usebackq tokens=1 delims=:" %%h in (`echo %TIME%`) do @set HOUR=%%h

SET Project=%1
SET ProjectDir=%2
SET BuiltOuputPath=%3
SET Config=%4
SET ZIP=C:\Projects\activiser\BuildSupport\7z.EXE u -tzip 

echo %Project% %BaseVersion%.%BuildNumber%

GOTO %Project%
:WebService
"C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE Property SET Value=0 WHERE Property='FolderForm_AllUsersVisible'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')" "UPDATE Control SET Width=290 WHERE Control='BannerText'" "UPDATE Control SET Attributes=2097159 WHERE (Dialog_='CustomTextC' AND Control='Edit4') OR (Dialog_='CustomTextA' AND Control='Edit3')"
SET SetupFolder=Web Service
SET Target=C:\Setup\Activiser\%BaseVersion%\%BuildNumber%.%HOUR%\%SetupFolder%\
IF NOT EXIST "%Target%" mkdir "%Target%"
copy "%ProjectDir%\Release\*" "%Target%"
%ZIP% "%Target%\WebServiceDlls.ZIP" "C:\Projects\activiser\WebService\WebService\bin\*.dll" 
GOTO END

:OutlookAddIn
echo "C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
"C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
SET SetupFolder=Outlook 2003 Client
SET Target=C:\Setup\Activiser\%BaseVersion%\%BuildNumber%.%HOUR%\%SetupFolder%\
IF NOT EXIST "%Target%" mkdir "%Target%"
copy "%ProjectDir%.\%Config%\*" "%Target%"
copy "C:\Projects\activiser\OutlookAddIn\activiser.OutlookAddIn.Setup\%Config%\setup.exe" "%Target%"
GOTO END


:WMClient
"C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE Property SET Value=0 WHERE Property='FolderForm_AllUsersVisible'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
SET SetupFolder=Windows Mobile Client
SET Target=C:\Setup\Activiser\%BaseVersion%\%BuildNumber%.%HOUR%\%SetupFolder%\
IF NOT EXIST "%Target%" mkdir "%Target%"
copy "%ProjectDir%\Release\*" "%Target%"
REM copy "C:\Projects\activiser\WindowsMobileClient\Setup\Release\activiser.CAB" "%Target%"
REM copy "C:\Projects\activiser\WindowsMobileClient\SetupCompressed\Release\activiserCompressed.CAB" "%Target%"
GOTO END

:Console
rem echo "C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
rem "C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
SET Target=C:\Setup\Activiser\%BaseVersion%\%BuildNumber%.%HOUR%\Console\
IF NOT EXIST "%Target%" mkdir "%Target%"
@ECHO ON
XCOPY /F /y %BuiltOuputPath%\Setup.exe "%Target%"
XCOPY /F /y %BuiltOuputPath%\en-us\activiserWorkstationComponents.msi "%Target%"
@GOTO END

:CustomFormDesigner
echo "C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
"C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
SET SetupFolder=Custom Form Designer
GOTO DOCOPY

:CrmPlugIn
echo "C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
"C:\Projects\activiser\BuildSupport\wirunsql.vbs" "%BuiltOuputPath%" "UPDATE Property SET Value=1 WHERE Property='ALLUSERS'" "UPDATE TextStyle SET FaceName='Arial' WHERE Size=12" "UPDATE TextStyle SET FaceName='Lucida Sans Unicode' WHERE Size=9 AND (TextStyle='VsdDefaultUIFont.524F4245_5254_5341_4C45_534153783400' OR TextStyle='VSI_MS_Sans_Serif13.0_0_0' OR TextStyle='VSI_MS_Shell_Dlg13.0_0_0')"
SET SetupFolder=CRM PlugIn
GOTO DOCOPY

:DOCOPY
SET Target=C:\Setup\Activiser\%BaseVersion%\%BuildNumber%.%HOUR%\%SetupFolder%\
IF NOT EXIST "%Target%" mkdir "%Target%"
copy "%BuiltOuputPath%" "%Target%"
GOTO END

:END
