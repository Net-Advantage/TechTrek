# Run the ./Login.ps1 script to login to Azure before running this script.
# ./DeployInfra.ps1 -baseName "mybicepapp" -deploymentKey "key"
# ./DeployInfra.ps1 -baseName "mybicepapp" -deploymentKey "key" -step "baseInfra"
# ./DeployInfra.ps1 -baseName "mybicepapp" -deploymentKey "key" -step "apps"
# ./DeployInfra.ps1 -baseName "mybicepapp" -deploymentKey "key" -step "baseInfra" -location "australiaeast" -environment "dev"

param (
    [string] $baseName,
    [string] $deploymentKey,
    [string] $step = "baseInfra",
    [string] $location = "australiaeast",
    [string] $environment = "dev"
)

# Note:
# The --location parameter here defines where the deployment metadata is stored and has no impact on the resources location specified in the bicep file.
$deploymentMetadataLocation = "australiaeast"

az deployment sub create `
--location $deploymentMetadataLocation `
--template-file ./_deploy.bicep `
--parameters baseName=$baseName deploymentKey=$deploymentKey step=$step location=$location environment=$environment