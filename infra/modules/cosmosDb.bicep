param sharedConfig object

resource cosmosDb 'Microsoft.DocumentDB/databaseAccounts@2021-06-15' = {
  name: 'cdb-techtrek-aueast-${sharedConfig.environment}'
  location: sharedConfig.location
  properties: {
    databaseAccountOfferType: 'Standard' // Update as needed
    // Additional properties as required
  }
}
