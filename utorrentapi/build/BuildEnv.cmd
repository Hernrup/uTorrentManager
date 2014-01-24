@ECHO OFF
pushd "%~dp0\..\src"
powershell.exe -NoExit -File "%~dpn0.ps1"
popd