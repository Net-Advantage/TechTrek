# Aspire Guidance

# Objective

- Learn how to deploy an Aspire-enabled application to Azure.

# Learnings

## What version of `azd` do I need?

From PowerShell, run the following command to see what version of `azd` you have installed:

```PowerShell
azd version
```
You need at least version `1.5.0` and above. This is also known as the [November 2023 Release](https://devblogs.microsoft.com/azure-sdk/azure-developer-cli-azd-november-2023-release/). However, keep an eye out for the latest version since you should anticipate updates to appear at regular intervals.

Run this command to __install__ the latest version:
```PowerShell
winget install microsoft.azd
```
Run this command to __upgrade__ to the latest version: 
```PowerShell
winget upgrade microsoft.azd
```

## What is the `azd` or the Azure Developer CLI?

https://aka.ms/azd


## What is the deployment manifest and why do I need it?

You need to generate a deployment manifest for your application. This manifest is a YAML file that contains the information that `azd` needs to deploy your application to Azure. The manifest contains the following information:

> NOTE: At the command prompt ensure that you are in the folder that contains your Aspire Host project. For TechTrek it is the project called `Nabs.TechTrek.AppHost`

Here is the command you need to run to allow `azd`` to learn about your project and to generate the manifest:
```PowerShell
azd init
```
Select the following options when prompted:
```
> Use code in the current directory
```
Then select:
```
> Confirm and continue initializing my app
```
Then select which service to expose to the internet:
```
[ ] techTrekGateway
```

## How can I view or verify what `azd` knows about my project?
This command will provide information about your project:
```PowerShell
azd show
```
