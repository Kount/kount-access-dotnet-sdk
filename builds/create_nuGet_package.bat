rem -----------------------------------------------------------------------
rem <copyright file="create_nuGet_package.bat" company="Kount Inc">
rem     Copyright Kount Inc. All rights reserved.
rem </copyright>
rem -----------------------------------------------------------------------

cd /D "%~dp0"
@echo Current directory is: %CD%

rem run nuget.exe
rem nuget pack ..\KountAccessSdk\KountAccessSdk.csproj

Start /wait MSBuild.exe "..\KountAccessSdk.sln" /t:Build /p:Configuration=Release /p:TargetFramework=v4.5

Start /wait nuget4.7.0.exe pack -Build "..\KountAccessSdk\KountAccessSdk.nuspec" -OutputDirectory "..\KountAccessSdk\bin\Publish" -Properties Configuration=Release;description="Kount Access .Net SDK";title="Kount Access .Net SDK"
