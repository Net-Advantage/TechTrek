# Compare the packages in a Directory.Packages.props file 
# with the packages that are actually referenced in the project files.

# Define the path to your solution directory and the Directory.Packages.props file
$rootDirectory = Get-Location
$packagesPropsFile = [System.IO.Path]::Combine($rootDirectory, "src", "Directory.Packages.props") 

# Parse the Directory.Packages.props to get a list of defined packages
[xml]$packagesProps = Get-Content $packagesPropsFile
$definedPackages = $packagesProps.Project.ItemGroup.PackageVersion | ForEach-Object { $_.Include }

# Find all project files in the solution directory
$projectFiles = Get-ChildItem -Path $rootDirectory -Recurse -Filter "*.csproj"

# Initialize an empty hash set to store referenced packages
$referencedPackages = @{}
# Initialize a dictionary to hold project references
$projectReferences = @{}

# Parse each project file to find package references
foreach ($projectFile in $projectFiles) {
    [xml]$projectContent = Get-Content $projectFile.FullName
    # Initialize an array to store current project references
    $currentProjectReferences = @()
    $projectContent.Project.ItemGroup.PackageReference | ForEach-Object {
        if ($null -ne $_.Include) {
            $referencedPackages[$_.Include] = $true
            $currentProjectReferences += $_.Include
        }
        else {
            Write-Host "Project reference without 'Include' attribute found in $($projectFile.Name)"
        }
    }
    # Store the current project's references in the dictionary
    $projectReferences[$projectFile.Name] = $currentProjectReferences
}

# Compare the lists to find packages that can potentially be removed
$removablePackages = @()
foreach ($package in $definedPackages) {
    if (-not $referencedPackages.ContainsKey($package)) {
        $removablePackages += $package
    }
}

# Output the potentially removable packages
if ($removablePackages.Count -eq 0) {
    Write-Host "No removable packages were found."
} else {
    Write-Host "Potentially removable packages:"
    $removablePackages | ForEach-Object { Write-Host "- $_" }
}

# Output project references
Write-Host ""
Write-Host "==================================================="
Write-Host "Project references:"
foreach ($project in $projectReferences.Keys) {
    Write-Host "Project: $project"
    foreach ($reference in $projectReferences[$project]) {
        Write-Host " - $reference"
    }
}
