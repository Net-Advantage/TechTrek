param sharedConfig object

// Modules
module storageAccount 'modules/storageAccount.bicep' = {
  name: 'storageAccountDeployment'
  params: {
    sharedConfig: sharedConfig
  }
}

module containerAppResources 'modules/containerAppEnvironmentResources.bicep' = {
  name: 'containerAppResourcesDeployment'
  params: {
    sharedConfig: sharedConfig
  }
}

// module keyVault 'modules/keyVault.bicep' = {
//   name: 'keyVaultDeployment'
//   params: {
//     sharedConfig: sharedConfig
//   }
//   scope: resourceGroup
// }

// module appConfiguration 'modules/appConfiguration.bicep' = {
//   name: 'appConfigurationDeployment'
//   params: {
//     sharedConfig: sharedConfig
//   }
//   scope: resourceGroup
// }

// module cosmosDb 'modules/cosmosDb.bicep' = {
//   name: 'cosmosDbDeployment'
//   params: {
//     sharedConfig: sharedConfig
//   }
//   scope: resourceGroup
// }

// module sqlServer 'modules/sqlServer.bicep' = {
//   name: 'sqlServerDeployment'
//   params: {
//     sharedConfig: sharedConfig
//   }
//   scope: resourceGroup
// }
