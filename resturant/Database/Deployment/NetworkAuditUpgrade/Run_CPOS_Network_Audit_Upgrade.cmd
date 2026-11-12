@echo off
chcp 65001 >nul
title CPOS Network Audit Upgrade
powershell.exe -NoLogo -NoProfile -ExecutionPolicy Bypass -File "%~dp0CPOS_Network_Audit_Upgrade.ps1"
set "CPOS_UPGRADE_EXIT=%ERRORLEVEL%"
echo.
if not "%CPOS_UPGRADE_EXIT%"=="0" echo Upgrade failed. Read the log file before opening CPOS.
if "%CPOS_UPGRADE_EXIT%"=="0" echo Upgrade completed successfully.
pause
exit /b %CPOS_UPGRADE_EXIT%

