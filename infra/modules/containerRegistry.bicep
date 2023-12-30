param sharedConfig object

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2021-06-01-preview' = {
  name: 'cr-techtrek-aueast-${sharedConfig.environment}'
  location: sharedConfig.location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}
