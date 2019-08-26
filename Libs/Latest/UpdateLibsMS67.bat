@echo off

call GetLibs.bat

powershell .\UpdateLibs.ps1 .\Libs.txt .\MS67.txt

call SetLibs.bat

pause