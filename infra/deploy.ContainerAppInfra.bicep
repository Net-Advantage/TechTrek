param sharedConfig object

module storageDataContributor 'Modules./Identities/storageDataContributorIdentity.bicep' = {
  name: 'storageDataContributor'
  params: {
    baseName: sharedConfig.baseName
    location: sharedConfig.location
  }
}

// module webApi 'Modules/containerApp.bicep' = {
//   name: 'web-api'
//   params: {
//     sharedConfig: sharedConfig
//     imageName: 'scaling-orleans.web-api'
//     //imageTag: 'latest'
//     allowExternalIngress: true
//     minReplicas: 1
//     maxReplicas: 2
//     userAssignedIerviceIdentityId: storageDataContributor.outputs.id
//     envVars : [

//     ]
//   }
// }
