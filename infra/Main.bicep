targetScope = 'subscription'

param tenantId string
param objectId string

param location string = 'australiaeast'
param environment string = 'dev'

var sharedConfig = {
  subscriptionId: subscription().subscriptionId
  tenantId: tenantId
  objectId: objectId
  location: location
  environment: environment
  keyVaultName: 'kv-techtrek-aueast-${environment}'
}


// Resource Group
resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: 'rg-techtrek-aueast-${environment}'
  location: location
}

// Modules

module storageAccount 'modules/storageAccount.bicep' = {
  name: 'storageAccountDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}

module containerRegistry 'modules/containerRegistry.bicep' = {
  name: 'containerRegistryDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}

module keyVault 'modules/keyVault.bicep' = {
  name: 'keyVaultDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}

module appConfiguration 'modules/appConfiguration.bicep' = {
  name: 'appConfigurationDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}

module cosmosDb 'modules/cosmosDb.bicep' = {
  name: 'cosmosDbDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}

module sqlServer 'modules/sqlServer.bicep' = {
  name: 'sqlServerDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}

module containerApp 'modules/containerApp.bicep' = {
  name: 'containerAppDeployment'
  params: {
    sharedConfig: sharedConfig
  }
  scope: resourceGroup
}
