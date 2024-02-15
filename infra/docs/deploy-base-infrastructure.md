# TechTrek Infrastructure

## Deploy the Base Infrastructure

The first phase is to deploy the base infrastructure. This operation should only be performed once.

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
