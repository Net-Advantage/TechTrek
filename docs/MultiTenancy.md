# Multi-Tenancy in TechTrek

Multi-tenancy is the concept of having multiple tenants or customers make use of an application. TechTrek is designed to be multi-tenant. This section will discuss the concept of multi-tenancy and how it is implemented.

TechTrek support three types of multi-tenancy:

Option | Application Isolation | Storage Isolation | Infrastructure | Description
--------------------- | --------------------- | ----------------- | -------------- | -----------
#1 | Shared | Shared | Shared | All tenants share the same application, storage, and infrastructure.
#2 | Shared | Dedicated | Shared | All tenants share the same application and infrastructure, but have dedicated storage.
#3 | Dedicated | Dedicated | Dedicated | Each tenant has their own application, storage, and infrastructure.

Customers can choose which option they want to use. The option is chosen when the customer signs up for the service. The option can be changed at any time. However, the change will only take effect at the end of the current billing cycle. It also involves down-time as the application will need to be re-deployed and the data migrated. This is not a free service and will incur a cost.

Each option has some considerations for the customer:

- __Option 1__. Only customers that do not have a requirement for data isolation should choose this option. This option is also ideal for customers who want to get started easily or trial the product.
- __Option 2__. Customers that have a requirement for data isolation should choose this option. Currently only Australia and New Zealand regions are supported for this option. We are able to support other regions, but this will incur a cost.
- __Option 3__. Customers that have a requirement for data isolation and have a requirement for dedicated infrastructure should choose this option. This option is also ideal for customers who have a requirement for customisation. Customisation should not directly modify TechTrek code. Instead, customisation should be done via the extension points provided by TechTrek.

# Multi-Tenancy Theory

This section will discuss the concept of multi-tenancy and how it is implemented in TechTrek.

## Logical Separation

Logical separation is the concept of having multiple tenants use the same shared infrastucture.

- Database separation is achieved in two ways:
    - Option 1: Single database with shared tables. Each table contain a `TenantId` column, the value of which is tenant-specific. This column is a reference or foreign key to the `Tenant` table. There is only a single configuration for the database.
    - Option 2: Seperate database for each tenant on the same infrastructure. Each database has their own connection string. The application will need to have a way to determine which connection string to use based on the tenant making the request.
- Storage separation is achieved in two ways:
    - by having a single storage account with a seperate tenant 'folder' that contains all tenant-specific files. This folder is named after the tenant. For example, if the tenant is called `Acme`, then the folder will be called `Acme`. The `TenantId` could also be used as a folder name.
    - by having a seperate storage account for each tenant.
- Compute separation is achieved in two ways:
    - by having a single-shared instance of the application ensuring that the application is tenant-aware. This means that the application can determine which tenant is making the request and respond accordingly. This tenant-awareness adds significant complexity to the application.
    - by having a seperate instance of the application for each tenant. This means that the application is significantly less complex.

In all of the cases above, option 2 is the preferred option for the logical separation scenario as it is:
    - more secure
    - more performant
    - code is less complex
    - infrastructure and configuration is more complex
    - Can be slightly more expensive

## Physical Separation

Physical separation is the concept of having multiple tenants use dedicated infrastructure. This is the most secure, performant, and, as you can imagine,most expensive option.

Separation is achieved by having seperate infrastructure for databases, storage, and compute.

Database servers and storage accounts are dedicated to a single tenant. Each will have their own connection strings and/or access keys.

Configuration of the application is more complex as each tenant requires a seperate configuration. The application will need to be able to determine which configuration to use based on the tenant.

Application logic does not have to deal with the complexity of multi-tenancy, the infrastructure and configuration is more complex.

Deployment of the application is more complex as each tenant requires a seperate deployment and cadence.

Operationally, this is the most complex option as each tenant requires a seperate set of operations.

## Hybrid Separation

There are several ways to implement hybrid separation:

- In the case of a shared SaaS application model, the compute can be shared by multiple tenants while the storage is separated from other tenants - this is called data isolation. This option is chosen when the tenant is cost constrained, but has a requirement for data isolation.
- In the case of a dedicated SaaS application model, the compute and the storage is dedicated to a single tenant.

## Tenant Identification

The tenant in TechTrek is identified by the tenantId. This is a GUID that is generated when the tenant is created. This GUID is used to identify the tenant in the database, storage, and compute. We use a GUID as it is globally unique and is not easily guessable. It also obfuscates the specific tenant being requested and hides the number of tenants in the system.

TechTrek uses a TenantContext to store the tenantId. This is a scoped service that is created when the request is received and is disposed of when the request is completed. This service is used to determine the tenantId for the request. Any part of the system can access the tenantId by injecting the TenantContext service. However, the goal is that developers do not have to contend with the tenantId as the system will automatically determine the tenantId for the request.

## User Identification

The user in TechTrek is identified by the userId. This is a GUID that is generated when the user is created. This GUID is used to identify the user in the database, storage, and compute. We use a GUID as it is globally unique and is not easily guessable. It also obfuscates the specific user being requested and hides the number of users in the system.

TechTrek uses a UserContext to store the userId. This is a scoped service that is created when the request is received and is disposed of when the request is completed. This service is used to determine the userId for the request. Any part of the system can access the userId by injecting the UserContext service. However, the goal is that developers do not have to contend with the userId as the system will automatically determine the userId for the request.

## Tenant and User Identification

Determining the Tenant and User in TechTrek is the concern of the Gateway.

TechTrek supports two types of access to the system:
- Anonymous access
- Authenticated access

Anonymous access is used when the user is not logged in. This is the case when the user is accessing the public pages of the application. In this case, the user is not identified and the tenant is identified by a special token in the URL. This token could be the TenantId or a special token that is mapped to the TenantId. This token is used to identify the tenant in the system. For example, the URL could be `https://techtrek.com/tenantId/...` or `https://techtrek.com/tenantToken/...` or `https://tenantToken.techtrek.com/...`.

