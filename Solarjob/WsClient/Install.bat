@echo off

echo. 
echo Install WsClient with Windows Service
echo for Solarjob
echo. 

SET PROG=%~WsClient.exe
SET FIRSTPART=%WINDIR%"\Microsoft.NET\Framework64\v"
SET SECONDPART="\InstallUtil.exe"

SET DOTNETVER=4.0.30319
  IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install

SET DOTNETVER=2.0.50727
  IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install

SET DOTNETVER=1.1.4322
  IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install

SET DOTNETVER=1.0.3705
  IF EXIST %FIRSTPART%%DOTNETVER%%SECONDPART% GOTO install

GOTO fail
:install
  ECHO Platform 64
  ECHO Found .NET Framework version %DOTNETVER%
  ECHO Installing service %PROG%
  ECHO ---------------------------------------------------
  %FIRSTPART%%DOTNETVER%%SECONDPART% %PROG%
  ECHO ---------------------------------------------------
  GOTO end
:fail
  echo FAILURE -- Could not find .NET Framework install
:param_error
  echo USAGE: installNETservie.bat [install type (I or U)] [application (.exe)]
:end
  ECHO DONE!!!
  Pause
