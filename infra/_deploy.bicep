targetScope = 'subscription'

@allowed(['baseInfra','apps'])
param step string = 'baseInfra'

@minLength(3)
@maxLength(10)
@description('The base name to use for the deployment.')
param baseName string
@minLength(2)
@maxLength(3)
@description('The unique key to use for the deployment which will form part of the resource name.')
param deploymentKey string
// param tenantId string
// param objectId string
param location string

@minLength(3)
@maxLength(4)
@description('The unique key to use for the deployment which will form part of the resource name.')
@allowed(['dev','uat','prod'])
param environment string = 'dev'

var sharedConfig = {
  subscriptionId: subscription().subscriptionId
  baseName: baseName
  deploymentKey: deploymentKey
  location: location
  environment: environment
  // tenantId: tenantId
  // objectId: objectId
  resourceGroupName: 'rg-${baseName}-${deploymentKey}-${environment}'
  storageAccountName: 'sa${baseName}${deploymentKey}${environment}'
  logAnalyticsWorkspaceName: 'law-${baseName}${deploymentKey}${environment}'
  appInsightsName: 'ai-${baseName}${deploymentKey}${environment}'
  containerRegistryName: 'cr${baseName}${deploymentKey}${environment}'
  containerAppEnvironmentName: 'cae-${baseName}${deploymentKey}${environment}'
}

resource resourceGroup 'Microsoft.Resources/resourceGroups@2023-07-01' = {
  name: sharedConfig.resourceGroupName
  location: sharedConfig.location
}

module baseInfraModule 'deploy.BaseInfra.bicep' = if (step == 'baseInfra') {
  name: 'baseInfraDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}

module appsModule 'deploy.ContainerAppInfra.bicep' = if (step == 'apps') {
  name: 'appsDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}
