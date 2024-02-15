# Infrastruture-as-code (IaC)

## Table of Contents

- [The importance of selecting the correct infrastucture](./selecting-the-correct-subscription.md)
- [Introduction to Deploying the TechTrek Sample Application](./introduction.md)
- [Deploy the Base Infrastructure](./deploy-base-infrastructure.md)


## Structure of the Infra Folder

- PowerShell Scripts (`./*.ps1`)
    - `Login.ps1`
    - `Phase1-DeployBaseInfrastructure.ps1`
    - `Phase2-BuildAndDeployApplicationImages.ps1`
    - `Phase3-DeployContainersToContainerAppEnvironment.ps1`
- Deployment Bicep Files (`./deploy.*.bicep`)
    - `deploy.baseInfra.bicep`
- Modules (`./modules/*.bicep`)
    - `appConfiguration.bicep`
    - `containerApp.bicep`
    - `containerRegistry.bicep`
    - `cosmosDb.bicep`
    - `keyVault.bicep`
    - `sqlServer.bicep`
    - `storageAccount.bicep`
- Identities (`./modules/identities/*Identity.bicep`)
    - 
- Docs (`./docs/*.md`)

