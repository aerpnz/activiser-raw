@ECHO OFF
SET BaseVersion=4.0.0
SET ActiviserVersion=Activiser%BaseVersion%
PATH C:\Projects\activiser\BuildSupport\;%PATH%

for /f "usebackq" %%v in (`GetBuildNumber.exe 2004-04-01`) do @set BuildNumber=%%v
ECHO %BuildNumber%

for /f "usebackq tokens=1 delims=:" %%h in (`echo %TIME%`) do @set HOUR=%%h

SET Project=%1
SET Config=%2
SET ProjectDir=%3
SET BuiltOuputPath=%4

echo %Project% %BaseVersion%.%BuildNumber%


GOTO %Project%
:WMClient

CD /D "%ProjectDir%\CabWiz"

GOTO %Config%
:Release
SET Filename=activiser
GOTO DOIT

:Debug
SET Filename=activiserDebug
GOTO DOIT

:DOIT
@ECHO ON
C:\Projects\activiser\BuildSupport\cabwiz.exe %FileName%.inf /compress
@ECHO OFF

SET SetupFolder=Windows Mobile Client
SET Target=C:\Setup\Activiser\%BaseVersion%\%BuildNumber%.%HOUR%\%SetupFolder%\
IF NOT EXIST "%Target%" mkdir "%Target%"
@ECHO ON
COPY %FileName%.cab "%Target%"
@ECHO OFF

:END
