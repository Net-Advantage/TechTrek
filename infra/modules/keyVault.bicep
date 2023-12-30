param sharedConfig object

resource keyVault 'Microsoft.KeyVault/vaults@2021-06-01-preview' = {
  name: sharedConfig.keyVaultName
  location: sharedConfig.location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: sharedConfig.tenantId
    accessPolicies: [
      {
        tenantId: sharedConfig.tenantId
        objectId: sharedConfig.objectId
        permissions: {
          secrets: [
            'get'
            'list'
          ]
        }
      }
    ]
  }
}

var secretNames = [
  'key1'
  'key2'
]

resource secrets 'Microsoft.KeyVault/vaults/secrets@2021-06-01-preview' = [
  for secretName in secretNames: {
    name: secretName
    properties: {
      attributes: {
        enabled: true
      }
    }
    parent: keyVault
  }
]

