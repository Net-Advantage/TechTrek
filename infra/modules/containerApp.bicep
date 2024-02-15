param sharedConfig object
param userAssignedIerviceIdentityId string
param allowExternalIngress bool
param targetIngressPort int = 80
param imageName string
param envVars array = []
param minReplicas int = 1
param maxReplicas int = 1
param imageTag string = 'latest'

resource acrPullIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-07-31-preview' = {
  name: 'sid-${sharedConfig.baseName}-AcrPullIdentity'
  location: sharedConfig.location
}

resource AcrPullRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(resourceGroup().id, sharedConfig.baseName, 'AcrPull')
  scope: resourceGroup()
  properties: {
    roleDefinitionId: resourceId('Microsoft.Authorization/roleDefinitions', '7f951dda-4ed3-4680-a7ca-43fe172d538d') // AcrPull role definition ID
    principalId: acrPullIdentity.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' existing = {
  name: sharedConfig.containerRegistryName
}

resource containerAppEnvironment 'Microsoft.App/managedEnvironments@2023-08-01-preview' existing = {
  name: sharedConfig.containerAppEnvironmentName
}

resource containerApp 'Microsoft.App/containerApps@2023-08-01-preview' ={
  name: sharedConfig.containerAppEnvironmentName
  location: sharedConfig.location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
          '${userAssignedIerviceIdentityId}': {}
      }
    }
  properties:{
    managedEnvironmentId: containerAppEnvironment.id
    configuration: {
      registries: [
        {
          server: containerRegistry.name
          identity: acrPullIdentity.id
        }
      ]
      ingress: {
        external: allowExternalIngress
        targetPort: targetIngressPort
      }
    }
    template: {
      containers: [
        {
          image: '${containerRegistry.name}/${imageName}:${imageTag}'
          name: imageName
          env: envVars
        }
      ]
      scale: {
        minReplicas: minReplicas
        maxReplicas: maxReplicas
        rules: [
          {
            name: 'cpu-auto-scaling'
            custom: {
              type: 'http'
              metadata: {
                concurrentRequests: '50' // Define the number of concurrent requests per instance before scaling out
              }
            }
          }
        ]
      }
    }
  }
}

output principalId string = containerApp.identity.principalId
