# Set console colors to defaults
$Host.UI.RawUI.ForegroundColor = 'White'
$Host.UI.RawUI.BackgroundColor = 'Black'
Clear-Host

# Define paths for coverage reports and test results
$rootDirectory = Get-Location
$testResultsPath = "TestResults"
$coverageReportPath = "coveragereport"
$solutionName = "./src/Nabs.TechTrek.sln"

# Clean up old test results and coverage reports
Get-ChildItem -Path $rootDirectory -Recurse -Filter $testResultsPath -Directory | ForEach-Object {
    Remove-Item -Path $_.FullName -Recurse -Force
    Write-Host "Removed TestResults directory at: $($_.FullName)"
}

if (Test-Path $coverageReportPath) {
    Remove-Item -Path $coverageReportPath -Recurse -Force
    Write-Host "Removed Coverage Report directory"
}

dotnet restore $solutionName --configfile ./src/nuget.config
dotnet build $solutionName --configuration Release --no-restore
dotnet test $solutionName --configuration Release --no-restore --no-build --logger "console;verbosity=detailed" --logger "trx;LogFileName=TestResults/testresults.trx" --collect:"XPlat Code Coverage" --settings src/coverlet.runsettings

dotnet tool install --global dotnet-reportgenerator-globaltool

reportgenerator -reports:./**/TestResults/*/coverage.cobertura.xml -targetdir:$coverageReportPath -reporttypes:Html
reportgenerator -reports:./**/TestResults/*/coverage.cobertura.xml -targetdir:$coverageReportPath -reporttypes:MarkdownSummaryGithub

Start-Process -FilePath (Join-Path -Path $coverageReportPath -ChildPath "index.html")

# Set console colors to defaults
$Host.UI.RawUI.ForegroundColor = 'White'
$Host.UI.RawUI.BackgroundColor = 'Black'