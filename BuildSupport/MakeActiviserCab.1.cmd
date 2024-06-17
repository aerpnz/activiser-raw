@ECHO OFF
SET BaseVersion=4.0.0
SET ActiviserVersion=Activiser%BaseVersion%
PATH C:\Projects\activiser\BuildSupport\;%PATH%

for /f "usebackq" %%v in (`GetBuildNumber.exe 2004-04-01`) do @set BuildNumber=%%v

for /f "usebackq tokens=1 delims=:" %%h in (`echo %TIME%`) do @set HOUR=%%h

SET Project=%1
SET Config=%2
SET ProjectDir=%3
SET BuiltOuputPath=%4

echo %Project% %BaseVersion%.%BuildNumber%


CD /D "%ProjectDir%\CabWiz"
GOTO %Project%%Config%

:WMClientRelease
SET SetupFolder=Windows Mobile Client
SET Filename=activiser
cabwiz.exe %FileName%.inf /compress
SET TargetCab=activiser.CAB
GOTO COPYTOSETUP

:WMClientDebug
SET SetupFolder=Windows Mobile Client
SET Filename=activiserDebug
cabwiz.exe %FileName%.inf /compress
SET TargetCab=activiserDebug.CAB
GOTO COPYTOSETUP

:MPClientRelease
SET SetupFolder=Minor Planet Client
SET Filename=activiser
cabwiz.exe %FileName%.inf 
SET TargetCab=activiserMP.CAB
GOTO COPYTOSETUP

:MPClientDebug
SET SetupFolder=Minor Planet Client
SET Filename=activiserDebug
cabwiz.exe %FileName%.inf 
SET TargetCab=activiserDebugMP.CAB
GOTO COPYTOSETUP

:COPYTOSETUP

SET Target=C:\Setup\Activiser\%BaseVersion%\%BuildNumber%.%HOUR%\%SetupFolder%\
IF NOT EXIST "%Target%" mkdir "%Target%"
@ECHO ON
COPY %FileName%.cab "%Target%\%TargetCab%"
@ECHO OFF

:END
