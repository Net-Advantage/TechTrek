dotnet restore src/Nabs.TechTrek.sln --configfile ./src/nuget.config
dotnet build src/Nabs.TechTrek.sln --configuration Release --no-restore
& "./src/Persistence/Nabs.TechTrek.PersistenceCli/ResetDatabases.ps1"
dotnet test src/Nabs.TechTrek.sln --configuration Release --no-restore --no-build --logger "console;verbosity=detailed" --settings src/coverlet.runsettings
dotnet test src/Nabs.TechTrek.sln --settings src/coverlet.runsettings
& "$env:UserProfile/.nuget/packages/reportgenerator/5.2.0/tools/net8.0/ReportGenerator.exe" -reports:"**/TestResults/*/coverage.cobertura.xml" -targetdir:"coveragereport"