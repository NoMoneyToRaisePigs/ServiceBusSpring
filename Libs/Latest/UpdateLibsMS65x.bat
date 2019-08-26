@echo off

call GetLibs.bat

powershell .\UpdateLibs.ps1 .\Libs.txt .\MS65x.txt

call SetLibs.bat

pause