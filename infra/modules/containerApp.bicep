param sharedConfig object

resource containerApp 'Microsoft.Web/containerApps@2021-03-01' = {
  name: 'ca-techtrek-aueast-${sharedConfig.environment}'
  location: sharedConfig.location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    // Container app specific properties
  }
}

output principalId string = containerApp.identity.principalId
