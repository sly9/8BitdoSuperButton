@echo off
setlocal enabledelayedexpansion

echo ==========================================
echo      8BitdoSuperButton Build Script
echo ==========================================

:: 1. Check for NuGet
set "NUGET_PATH=tools\nuget.exe"
set "NUGET_URL=https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

if not exist "%NUGET_PATH%" (
    echo [INFO] nuget.exe not found. Downloading...
    if not exist "tools" mkdir "tools"
    powershell -Command "Invoke-WebRequest '%NUGET_URL%' -OutFile '%NUGET_PATH%'"
    if !errorlevel! neq 0 (
        echo [ERROR] Failed to download nuget.exe.
        pause
        exit /b 1
    )
    echo [INFO] nuget.exe downloaded.
) else (
    echo [INFO] nuget.exe found.
)

:: 2. Restore Packages
echo [INFO] Restoring NuGet packages...
"%NUGET_PATH%" restore 8BitdoSuperButton.sln
if !errorlevel! neq 0 (
    echo [ERROR] NuGet restore failed.
    pause
    exit /b 1
)

:: 3. Find MSBuild
set "MSBUILD_PATH="

:: Try vswhere
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" (
    for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do (
        set "MSBUILD_PATH=%%i"
    )
)

:: Fallback to specific paths if vswhere fails or isn't present
if not defined MSBUILD_PATH (
    if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
    ) else if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" (
        set "MSBUILD_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
    ) else if exist "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" (
        set "MSBUILD_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"
    )
)

if not defined MSBUILD_PATH (
    echo [ERROR] MSBuild.exe not found. Please ensure Visual Studio or Build Tools is installed.
    pause
    exit /b 1
)

echo [INFO] Using MSBuild at: "!MSBUILD_PATH!"

:: 4. Build Solution
echo [INFO] Building solution in Release configuration...
"!MSBUILD_PATH!" 8BitdoSuperButton.sln /p:Configuration=Release /t:Rebuild
if !errorlevel! neq 0 (
    echo [ERROR] Build failed.
    pause
    exit /b 1
)

echo ==========================================
echo           Build Successful!
echo ==========================================
echo Output located in: 8BitdoSuperButton\bin\Release\
pause
