@echo off

if "%1%"==""  goto usage
IF "_%2" == "_" goto usage

isql -version> nul 2>&1
if NOT "%ERRORLEVEL%"=="0"  goto setsybase


:runsql
isql -U%2 -P%3 -S%1 -o %TMP%\version.log < getversion.sql

rem If there is any error running isql display a message to user
IF NOT ERRORLEVEL 0 goto error

rem delete any existing tmp.bat
if  exist "%TMP%\tmp.bat" del %TMP%\tmp.bat

rem get only the version into the tmp.bat
findstr "ASEVERSION" %TMP%\version.log > %TMP%\tmp.bat

rem execute the  tmp.bat
call %TMP%\tmp.bat

rem Check if ASEVERSION is set properly
if "_%ASEVERSION%" == "_"  goto error

rem install commong stored procedures
isql -U%2 -P%3 -S%1 -o%TMP%\sp_drv_common.log < sp_drv_common.sql
IF NOT ERRORLEVEL 0 goto error
del /Q %TMP%\sp_drv_common.log

if "%ASEVERSION%" == "150" goto Runmda
echo "Installing sp_version..."
isql -U%2 -P%3 -S%1 -o %TMP%\sp_version.log < sp_version.sql
del /Q %TMP%\sp_version.log
goto Runmda

:Runmda
echo  Wait...!!! Running OLEDB Metadata SQL
isql -U%2 -P%3 -S%1 -o%TMP%\oledb_mda_%ASEVERSION%.log < oledb_mda_%ASEVERSION%.sql
IF NOT ERRORLEVEL 0 goto error
goto Success

:Success
echo OLEDB Metadata SQL's Completed Sucessfully
echo .
echo NOTE: If there are any errors Please check %TMP%\oledb_mda_%ASEVERSION%.log file
goto end

:usage
echo.
echo.
echo USAGE: install_oledb_sprocs ^<server^> ^<user^> [^<pass^>]
echo.
echo.
echo Example : Windows, This Batch can be run thru Windows Only
echo ---------------------------------------------------------------------
echo USAGE :install_oledb_sprocs  myaseserver login1 pass1  - where myaseserver is the ServerName running on machine: Mymachine
echo.
goto end

:error
 echo Not Able to get Connection to your ASE Server : ^<%1%^>
 echo Pls. Check the Parameters Passed are Correct or
 echo Check if ASE server is up and running...
 goto end

:setsybase
 if not exist "%SYBASE%\SYBASE.BAT" goto nosybase
 CALL "%SYBASE%\SYBASE.BAT"
 set PATH=%PATH%;%windir%\system32
 goto runsql
 
 :nosybase
 echo Either SYBASE is not installed of Envirnoment variable SYBASE is not Set...
 goto end

:end
set ASEVERSION=
del /Q %TMP%\tmp.bat
del /Q %TMP%\version.log 
