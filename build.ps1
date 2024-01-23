# Define paths for coverage reports and test results
$rootDirectory = Get-Location
$testResultsPath = "TestResults"
$coverageReportPath = "coveragereport"

# Clean up old test results and coverage reports
Get-ChildItem -Path $rootDirectory -Recurse -Filter $testResultsPath -Directory | ForEach-Object {
    Remove-Item -Path $_.FullName -Recurse -Force
    Write-Host "Removed TestResults directory at: $($_.FullName)"
}

if (Test-Path $coverageReportPath) {
    Remove-Item -Path $coverageReportPath -Recurse -Force
}

dotnet restore src/Nabs.TechTrek.sln --configfile ./src/nuget.config
dotnet build src/Nabs.TechTrek.sln --configuration Release --no-restore
dotnet test src/Nabs.TechTrek.sln --configuration Release --no-restore --no-build --logger "console;verbosity=detailed" --settings src/coverlet.runsettings
& "$env:UserProfile/.nuget/packages/reportgenerator/5.2.0/tools/net8.0/ReportGenerator.exe" -reports:"**/TestResults/*/coverage.cobertura.xml" -targetdir:"coveragereport"
