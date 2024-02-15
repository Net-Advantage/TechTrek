@minLength(3)
@maxLength(21)
param baseName string
param location string

resource serviceIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' = {
  name: 'sid-${baseName}-StorageDataContributorIdentity'
  location: location
}

resource blobDataContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(resourceGroup().id, baseName, 'Storage Blob Data Contributor')
  scope: resourceGroup()
  properties: {
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', 'ba92f5b4-2d11-453d-a403-e96b0029c9fe')
    principalId: serviceIdentity.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource tableDataContributorRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(resourceGroup().id, baseName, 'Storage Table Data Contributor')
  scope: resourceGroup()
  properties: {
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', '0a9a7e1f-b9d0-4cc4-a60d-0319b160aaa3') // Role definition ID for Storage Blob Data Contributor
    principalId: serviceIdentity.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

output principalId string = serviceIdentity.properties.principalId
output id string = serviceIdentity.id
output clientId string = serviceIdentity.properties.clientId
