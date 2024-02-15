param sharedConfig object

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-06-01' = {
  name: sharedConfig.storageAccountName
  location: sharedConfig.location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}
