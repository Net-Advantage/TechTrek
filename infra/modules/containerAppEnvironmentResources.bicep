param sharedConfig object

// Azure Azure Log Analytics workspace
resource logs 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: sharedConfig.logAnalyticsWorkspaceName
  location: sharedConfig.location
  properties: any({
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}

// Azure Application Insights
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: sharedConfig.appInsightsName
  location: sharedConfig.location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logs.id
  }
}

// Azure Container Registry
resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' = {
  name: sharedConfig.containerRegistryName
  location: sharedConfig.location
  sku: {
    name: 'Standard'
  }
}

// Azure Container Apps Environment
resource containerAppEnvironment 'Microsoft.App/managedEnvironments@2023-08-01-preview' = {
  name: sharedConfig.containerAppEnvironmentName
  location: sharedConfig.location
  properties: {
    daprAIInstrumentationKey:appInsights.properties.InstrumentationKey
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logs.properties.customerId
        sharedKey: logs.listKeys().primarySharedKey
      }
    }
  }
}

// TODO:
// Investigate configuration to isolate the enfironment within a VNET.
// Investigate security aspects, such as RBAC for managing access to the environment.
// Investigate setting up environment variables at the environment level.
