param sharedConfig object

resource sqlServer 'Microsoft.Sql/servers@2021-05-01-preview' = {
  name: 'sql-techtrek-aueast-${sharedConfig.environment}'
  location: sharedConfig.location
  properties: {
    administratorLogin: '<admin-username>' // Update this
    administratorLoginPassword: '<admin-password>' // Update this
    version: '12.0' // Update as needed
  }
}
