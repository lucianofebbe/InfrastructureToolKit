# Caminhos
$projectPath = "C:\Users\lucia\Documents\Empresa\Projetos\InfrastructureToolKit\InfrastructureToolKit"
$destFolder  = "C:\Users\lucia\Documents\Empresa\Arquiteturas\Nuget"
$configuration = "Release"

Write-Host ""
Write-Host "=== Etapa 1: Gerando pacote NuGet ===" -ForegroundColor Cyan
Set-Location $projectPath

# Empacotando o projeto
$packResult = dotnet pack -c $configuration

if ($LASTEXITCODE -ne 0) {
    Write-Host "`n❌ Erro ao gerar o pacote NuGet." -ForegroundColor Red
    Pause
    exit 1
}

# Encontrando o pacote gerado
$packageFile = Get-ChildItem -Path "$projectPath\bin\$configuration" -Filter "*.nupkg" -Recurse | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if (-not $packageFile) {
    Write-Host "`n❌ Nenhum arquivo .nupkg encontrado." -ForegroundColor Red
    Pause
    exit 1
}

# Criando pasta de destino se necessário
if (-not (Test-Path -Path $destFolder)) {
    New-Item -ItemType Directory -Path $destFolder | Out-Null
}

# Copiando o pacote
Write-Host "`nCopiando '$($packageFile.Name)' para '$destFolder'" -ForegroundColor Yellow
Copy-Item -Path $packageFile.FullName -Destination $destFolder -Force

if ($LASTEXITCODE -ne 0) {
    Write-Host "`n❌ Erro ao copiar o pacote." -ForegroundColor Red
    Pause
    exit 1
}

Write-Host "`n✅ Pacote copiado com sucesso!" -ForegroundColor Green
Write-Host "Caminho final: $destFolder\$($packageFile.Name)" -ForegroundColor Green
Pause
