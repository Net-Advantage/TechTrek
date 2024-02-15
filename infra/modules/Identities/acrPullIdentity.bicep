param sharedConfig object

resource serviceIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' = {
  name: 'sid-${sharedConfig.baseName}-AcrPullIdentity'
  location: sharedConfig.location
}

resource AcrPullRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(resourceGroup().id, sharedConfig.baseName, 'AcrPull')
  scope: resourceGroup()
  properties: {
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d') // AcrPull role definition ID
    principalId: serviceIdentity.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

output principalId string = serviceIdentity.properties.principalId
output id string = serviceIdentity.id
output clientId string = serviceIdentity.properties.clientId
