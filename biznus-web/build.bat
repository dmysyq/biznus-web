@echo off
cd biznus-web
dotnet build
if %ERRORLEVEL% EQU 0 (
    echo Build successful!
    dotnet run
) else (
    echo Build failed!
)
pause
