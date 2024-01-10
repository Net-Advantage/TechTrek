# Code Coverage

Here are the commands to run the code coverage tool locally:

```powershell
# run from the c:\dev\Nabs.TechTrek folder.

# Collect the code coverage data
dotnet test src/Nabs.TechTrek.sln --configuration Release --no-restore --no-build --logger "console;verbosity=detailed" --settings src/coverlet.runsettings

# Generate the coverage report
"$env:UserProfile\.nuget\packages\reportgenerator\5.2.0\tools\net8.0\ReportGenerator.exe" -reports:"**\TestResults\*\coverage.cobertura.xml" -targetdir:"coveragereport"
```

