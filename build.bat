@echo off
cls

:: Enable ANSI escape sequences (Windows 10+)
for /f "tokens=2 delims=:" %%i in ('"prompt $H & for %%b in (1) do rem"') do set "BS=%%i"
echo %BS%[0m >nul

:: Define color escape codes
set "RED=[91m"
set "GREEN=[92m"
set "YELLOW=[93m"
set "CYAN=[96m"
set "RESET=[0m"

echo.
echo  %CYAN%===============================================%RESET%
echo         %CYAN%AutoStroke Builder%RESET%
echo  %CYAN%===============================================%RESET%

:: Check if dotnet is installed
where dotnet >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo  %RED%[ERROR]%RESET% .NET SDK not found. Please install the .NET 6.0 SDK.
    echo         Download from: https://dotnet.microsoft.com/download/dotnet/6.0
    pause
    exit /b 1
)

:: Check .NET version
dotnet --version | findstr /B /C:"6." >nul
if %ERRORLEVEL% NEQ 0 (
    echo  %YELLOW%[WARNING]%RESET% You may not have .NET 6.0 SDK installed.
    echo             The build may fail or use a different .NET version.
    echo.
    choice /C YN /M "  Continue anyway? [Y/N]"
    if %ERRORLEVEL% NEQ 1 exit /b 1
)

echo.
echo  %CYAN%[1/4]%RESET% Cleaning previous build artifacts...
dotnet clean -c Release >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo        %YELLOW%[WARNING]%RESET% Clean operation had issues, continuing anyway...
)

echo.
echo  %CYAN%[2/4]%RESET% Building self-contained application...
dotnet publish -c Release >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo  %RED%[ERROR]%RESET% Build failed!
    dotnet publish -c Release
    pause
    exit /b 1
)
echo        %GREEN%Build successful.%RESET%

echo.
echo  %CYAN%[3/4]%RESET% Creating distribution...
if exist dist rmdir /s /q dist >nul 2>&1
mkdir dist >nul 2>&1

copy "AutoStroke\bin\Release\net6.0-windows\win-x64\publish\AutoStroke.exe" "dist\AutoStroke.exe" >nul
if exist "dist\Resources" rmdir /s /q "dist\Resources" >nul 2>&1
mkdir "dist\Resources" >nul 2>&1
copy "AutoStroke\Resources\app_icon.ico" "dist\Resources\app_icon.ico" >nul
if exist "AutoStroke\bin\Release\net6.0-windows\win-x64\publish\Resources\app_icon.ico" copy "AutoStroke\bin\Release\net6.0-windows\win-x64\publish\Resources\app_icon.ico" "dist\Resources\app_icon.ico" >nul 2>&1

if exist "dist\README.txt" copy "dist\README.txt" "dist\README.txt" >nul 2>&1
if exist "dist\RunAsAdmin.bat" copy "dist\RunAsAdmin.bat" "dist\RunAsAdmin.bat" >nul 2>&1

echo        %GREEN%Distribution created in 'dist' folder.%RESET%
echo        Executable: dist\AutoStroke.exe
echo.

choice /C YN /M "  Do you want to create a ZIP package? [Y/N]"
if %ERRORLEVEL% EQU 1 (
    echo        Creating ZIP package...
    powershell -command "Compress-Archive -Path dist\* -DestinationPath AutoStroke-Standalone.zip -Force" >nul 2>&1
    echo        %GREEN%ZIP package created: AutoStroke-Standalone.zip%RESET%
    echo.
    
    choice /C YN /M "  Keep only the ZIP file (delete dist folder)? [Y/N]"
    if %ERRORLEVEL% EQU 1 (
        rmdir /s /q dist >nul 2>&1
        echo        Dist folder removed, keeping only ZIP package.
    )
)

echo.
echo  %CYAN%[4/4]%RESET% Cleaning up build artifacts...
rmdir /s /q AutoStroke\bin\Debug >nul 2>&1
rmdir /s /q AutoStroke\bin\Release\net6.0-windows\publish >nul 2>&1
rmdir /s /q AutoStroke\bin >nul 2>&1
rmdir /s /q AutoStroke\obj >nul 2>&1
echo        All build folders cleaned.

echo.
echo  %CYAN%===============================================%RESET%
echo        %CYAN%Build process completed!%RESET%
echo  %CYAN%===============================================%RESET%
echo.
