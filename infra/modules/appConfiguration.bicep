param sharedConfig object

resource appConfiguration 'Microsoft.AppConfiguration/configurationStores@2021-03-01-preview' = {
  name: sharedConfig.containerAppEnvironmentName
  location: sharedConfig.location
  sku: {
    name: 'Standard' // Update as needed
  }
}

var appConfigSettings = [
  { 'AppKey1': 'appKey1Value' }
  { 'AppKey2': 'appKey2Value' }
]

resource appConfigKeyValues 'Microsoft.AppConfiguration/configurationStores/keyValues@2021-03-01-preview' = [
  for setting in appConfigSettings: {
    parent: appConfiguration
    name: setting.key
    properties: setting.value
  }
]

var keyVaultSecretNames = [
  'KeyVaultKey1'
  'KeyVaultKey2'
]

resource appConfigSecrets 'Microsoft.AppConfiguration/configurationStores/keyValues@2021-03-01-preview' = [
  for secretName in keyVaultSecretNames: {
    parent: appConfiguration
    name: secretName
    properties: {
      value: 'https://${sharedConfig.keyVaultName}.vault.azure.net/secrets/${secretName}/{SecretVersion}'
    }
  }
]
