@echo off
setlocal

rem === Caminhos ===
set "PROJECT_PATH=C:\Users\lucia\Documents\Empresa\Arquiteturas\InfrastructureToolKit\InfrastructureToolKit"
set "DEST_FOLDER=C:\Users\lucia\Documents\Empresa\Arquiteturas\Nuget"

rem === Configurações ===
set "CONFIGURATION=Release"

echo.
echo === Etapa 1: Gerando pacote NuGet ===
cd /d "%PROJECT_PATH%"
dotnet pack -c %CONFIGURATION%
if errorlevel 1 (
    echo.
    echo ❌ Erro ao gerar o pacote NuGet.
    pause
    exit /b 1
)

rem === Etapa 2: Encontrando o arquivo .nupkg ===
set "PACKAGE_FILE="
for /f "delims=" %%f in ('dir /b /s "%PROJECT_PATH%\bin\%CONFIGURATION%\*.nupkg"') do (
    set "PACKAGE_FILE=%%f"
)

if "%PACKAGE_FILE%"=="" (
    echo.
    echo ❌ Nenhum arquivo .nupkg encontrado.
    pause
    exit /b 1
)

rem === Etapa 3: Copiando para repositório local ===
echo.
echo Copiando "%PACKAGE_FILE%" para "%DEST_FOLDER%"
mkdir "%DEST_FOLDER%" >nul 2>&1
copy "%PACKAGE_FILE%" "%DEST_FOLDER%" /Y
if errorlevel 1 (
    echo.
    echo ❌ Erro ao copiar o pacote.
    pause
    exit /b 1
)

echo.
echo ✅ Pacote copiado com sucesso!
echo Caminho final: %DEST_FOLDER%\%~nxPACKAGE_FILE%

endlocal
pause

