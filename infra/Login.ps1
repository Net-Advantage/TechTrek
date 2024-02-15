# Usage:
# ./Login.ps1

# List all cached accounts
$accounts = az account list --all --output json | ConvertFrom-Json

# Display the option to log in as the first choice
Clear-Host

# Display cached accounts
Write-Host "Cached Accounts:"
for ($i = 0; $i -lt $accounts.Count; $i++) {
    $account = $accounts[$i]
    Write-Host "   $($i+1): User: $($account.user.name), Subscription Name: $($account.name), Subscription ID: $($account.id)"
}

# Prompt user to select an account or log in
$selection = Read-Host "Enter the number of the account you wish to use, or press Enter to log in with a new account"

# Check if input is empty and default to '0'
if ([string]::IsNullOrWhiteSpace($selection)) {
    $selectedIndex = -1  # Default to '0' which is log in with a new account
} else {
    $selectedIndex = $selection - 1
}

if ($selectedIndex -eq -1) {
    # User chose to log in with a new account
    az login
    # Capture the current user's information and the tenant ID from the login session
    $currentUserInfo = az account show --output json | ConvertFrom-Json
    $currentTenantId = $currentUserInfo.tenantId

    # List subscriptions only for the newly logged-in account and current tenant
    $subscriptions = az account list --all --output json | ConvertFrom-Json | Where-Object { $_.tenantId -eq $currentTenantId }

    Clear-Host
    Write-Host "Subscriptions for $($currentUserInfo.user.name) in Tenant ID: $currentTenantId :"
    for ($i = 0; $i -lt $subscriptions.Count; $i++) {
        $subscription = $subscriptions[$i]
        Write-Host "$($i+1): User: $($currentUserInfo.user.name), Subscription Name: $($subscription.name), Subscription ID: $($subscription.id)"
    }

    # Prompt user to select a subscription from the new account
    $subSelection = Read-Host "Enter the number of the subscription you wish to use"
    $selectedSubIndex = $subSelection - 1
    if ($selectedSubIndex -ge 0 -and $selectedSubIndex -lt $subscriptions.Count) {
        $selectedSubscription = $subscriptions[$selectedSubIndex]
        az account set --subscription $selectedSubscription.id
        Clear-Host
        Write-Host "Context set to $($selectedSubscription.name) - $($selectedSubscription.id)"
    } else {
        Clear-Host
        Write-Host "Invalid subscription selection. Exiting script."
        exit
    }
} elseif ($selectedIndex -ge 0 -and $selectedIndex -lt $accounts.Count) {
    $selectedAccount = $accounts[$selectedIndex]
    # Set the context to the selected account
    az account set --subscription $selectedAccount.id
    Clear-Host
    Write-Host "Context set to $($selectedAccount.name) - $($selectedAccount.id)"
} else {
    Clear-Host
    Write-Host "Invalid selection. Exiting script."
    exit
}
