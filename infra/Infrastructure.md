# TechTrek Infrastructure 

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


