#Requires -Version 7
<#
.SYNOPSIS
    Publica la web, la arranca, exporta el sitio estático a dist/ y detiene el servidor.

.EXAMPLE
    ./scripts/export-static.ps1 -BasePath /mi-repo/ -PublicUrl https://usuario.github.io
#>
[CmdletBinding()]
param(
    [string]$BasePath = "/",
    [string]$PublicUrl = "https://example.com",
    [int]$Port = 5199,
    [string]$Output = "dist"
)

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$project = Join-Path $root "src/Portfolio.Web"
$publish = Join-Path $project "bin/Release/net10.0/publish"
$log = Join-Path ([System.IO.Path]::GetTempPath()) "portfolio-static-export.log"

Write-Host "Publishing web app..." -ForegroundColor Cyan
dotnet publish $project -c Release --nologo

$env:ASPNETCORE_URLS = "http://localhost:$Port"
$env:ASPNETCORE_ENVIRONMENT = "Production"
$env:Portfolio__BasePath = $BasePath
$env:Portfolio__PublicBaseUrl = $PublicUrl

Write-Host "Starting published site..." -ForegroundColor Cyan
$server = Start-Process -FilePath "dotnet" -ArgumentList "Portfolio.Web.dll" `
    -WorkingDirectory $publish -PassThru -RedirectStandardOutput $log -RedirectStandardError "$log.err"

try {
    $ready = $false
    foreach ($attempt in 1..30) {
        try {
            Invoke-WebRequest -Uri "http://localhost:$Port$BasePath" -UseBasicParsing -TimeoutSec 5 | Out-Null
            $ready = $true
            break
        }
        catch {
            Start-Sleep -Seconds 1
        }
    }

    if (-not $ready) {
        throw "The published site did not become ready. See $log"
    }

    Write-Host "Exporting static site..." -ForegroundColor Cyan
    dotnet run --project (Join-Path $root "tools/Portfolio.StaticExporter") -c Release -- `
        --source "http://localhost:$Port" `
        --output (Join-Path $root $Output) `
        --static-dir (Join-Path $publish "wwwroot") `
        --base-path $BasePath `
        --public-url $PublicUrl

    Write-Host "Done. Static site available at $Output" -ForegroundColor Green
}
finally {
    if ($server -and -not $server.HasExited) {
        Stop-Process -Id $server.Id -Force
    }
}
