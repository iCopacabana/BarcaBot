$ErrorActionPreference = "Stop"

function PrepareDir {
    param (
        [string]$dirName
    )
    
    if (Test-Path .\$dirName) {
        Set-Location .\$dirName
        
        Remove-Item * -Force -Recurse

        Set-Location ..
    } else {
        mkdir $dirName
    }
}

Set-Location ..
Set-Location .\Barcabot

dotnet publish -c Release

Set-Location ..

PrepareDir("Bot")
PrepareDir("Hangfire")

[string]$source = ".\Barcabot\Barcabot.Bot\bin\Release\netcoreapp2.2\publish\*"
[string]$destination = ".\Bot\"

Move-Item -Force $source -Destination $destination

[string]$source = ".\Barcabot\Barcabot.HangfireService\bin\Release\netcoreapp2.2\publish\*"
[string]$destination = ".\Hangfire\"

Move-Item -Force $source -Destination $destination

Set-Location .\Scripts

Write-Output "`n`nDone.`n"