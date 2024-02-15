# TechTrek Infrastructure

## Introduction

This document aims to provide developers with comprehensive training on defining and deploying Azure infrastructure using Infrastructure as Code (IaC) principles, focusing on Bicep and PowerShell scripting. The goal is to equip our development teams with the necessary skills to build, manage, and deploy scalable, reliable, and secure infrastructure components.

> Pre-requisites: Familiarity with Azure services, PowerShell scripting, and Bicep.

## Infrastructure as Code (IaC)

Infrastructure as Code is a key practice in DevOps that involves managing and provisioning infrastructure through machine-readable definition files, rather than physical hardware configuration or interactive configuration tools such as the Azure Portal. IaC ensures that infrastructure deployment is repeatable, consistent, and can be automated, leading to more efficient and error-free deployments.


## Deployment Phases
### Phase 1: Deploy the Base Infrastructure

The base infrastructure lays the foundation for our application, ensuring that all necessary Azure resources are provisioned and configured correctly. This phase is critical as it sets up the environment where our application will run. It's important to note that this phase is typically executed once but can be repeated as needed for various scenarios such as disaster recovery, testing, or expansion.

## Phase 2: Build and Deploy Application Images

This phase focuses on continuous integration practices, where the application is built and tested. Upon successful testing, application images are created and pushed to the Azure Container Registry. This ensures that only tested and stable versions of the application are deployed.

> Key Points:
Highlight best practices for image tagging to manage versions effectively.
Discuss the integration of security practices like vulnerability scanning for container images.

## Phase 3: Deploy Containers to the Container App Environment

The final phase involves deploying the containerized applications to the Azure Container App Environment. This includes configuring access controls, networking, scaling, and monitoring settings to ensure the application runs efficiently and securely.

> Key Points:
Stress the importance of monitoring and logging for maintaining the health and performance of the application.
Discuss the use of managed identities for secure access to other Azure resources without storing credentials in code.

# Conclusion

This document is intended to serve as a guide for developers to understand and implement Infrastructure as Code practices for deploying Azure resources. Through this training, developers will gain hands-on experience in automating infrastructure deployment, which is crucial for efficient and scalable cloud development.



# TechTrek Infrastructure

This document provides detailed step-by-step training to developers on defining and deploying Azure infrastructure.

#GPT: Provide a brief overview of the importance of ensuring that the correct subscription is selected before deploying resources.

## Infrastructure as Code (IaC)
The deployment of the TechTrek infrastructure will work in three phases:
1. Deploy the base infrastructure.
1. Build and deploy the application images.
1. Deploy the containers to the container app environment.

### Base Infrastructure

The first phase is to deploy the base infrastructure. This operation should only be performed once.
It also allows us to redeploy our infrastructure if we need to in the case of:
- a disaster recovery scenario.
- an isolated environment is required for a tenant.
- setting up in a new region.
- setting up test environments.
- training developers on the infrastructure.

The base infrastructure is composed of the following Azure Resources:
- Resource Group - all resources will be deployed to a single resource group.
- Azure Storage Account - only blob and table storage will be used.
- Key Vault - used to store secrets and certificates.
- App Configuration - used to store application settings. We will attempt as far as possible to not to use environment variables in the container apps.
- Azure Container Registry - used to store the container images used by this application only. We will not use a public container registry.
- Azure Container App Environment - used to host the container apps in a single hosted environment.
- Azure SQL Server - used to host the databases for the application.
- Azure SQL Database - used to store the data for the application.
- Azure Cosmos DB - used to store the data for the application.
- User Assigned Managed Identities - uses RBAC to control access to the various parts of the application.
    - Storage Account Data Contributor Identity - used to read, write, and delete data in the storage account blob and tables.
    - ACR Pull Identity - used to pull images from the container registry.
    - SQL Server Contributor Identity - used to manage the SQL Server and databases.
    - SQL Server Data Contributor Identity - used to read, write, and delete data in the SQL databases.
    - Cosmos DB Contributor Identity - used to manage the Cosmos DB and databases.
    - Cosmos DB Data Contributor Identity - used to read, write, and delete data in the Cosmos DB databases.

It is during this phase that the user assigned managed identities will be created and assigned to the required resources.
- Azure Container App Environment - the ACR Pull Identity will be assigned to the Azure Container App Environment.

### Build and Deploy Application Images

The second phase is to build and test the solution, and then to build the images and deploy the application images to the Azure Container Registry.

### Deploy Containers to the Container App Environment

The third phase is to deploy the containers to the Azure Container App Environment.
It is during this phase that each application will be assigned the required user assigned managed identities.

## Params and Shared Configuration

The `Main.bicep` file contains the parameters and shared configuration that is used by all the modules.

Not all of the parameters are used by all the modules. The parameters are defined in the `Main.bicep` file to ensure that they are consistent across all the modules.

```bicep
param tenantId string
param objectId string

param location string = 'australiaeast'
param environment string = 'dev'

var sharedConfig = {
  subscriptionId: subscription().subscriptionId
  tenantId: tenantId
  objectId: objectId
  location: location
  environment: environment
}
```

## Modules

The resources must be created in a very specific order.

### Resource Group

### Key Vault (keyVault.bicep)

Access Policies are defined in the Key Vault module.

TODO: Add a section on how to add access policies to the Key Vault.
TODO: Need to understand how access policies work a bit more.

It is important to note that the secret names are defined in the `secretNames` variable. This is to ensure that the secret names are consistent across all the environments.

The values are not added directly to the bicep file. They need to be added in manually via the the portal or cli. Ensure that you create a secure process to manage your secrets.

If a new secret name is added to the `secretNames` variable, the new secret will be created in the Key Vault at the next deployment.

If an entry is removed from the `secretNames` array a deployment will __not__ remove the corresponding secret from the Key Vault. In order to remove a secret from the Key Vault, you will need to manually delete it using the portal or cli.

```azurecli
az keyvault secret delete --name <secret-name> --vault-name <key-vault-name>
```


Use RBAC where possible to manage access to your Azure resources instead of secrets such as connection strings.



### App Configuration (appConfiguration.bicep)


