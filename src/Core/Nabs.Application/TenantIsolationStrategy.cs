namespace Nabs.Application;

public enum TenantIsolationStrategy
{
    /// <summary>
    /// Shared Application, Shared Storage
    /// </summary>
    SharedShared,

    /// <summary>
    /// Shared Application, Dedicated Storage
    /// </summary>
    SharedDedicated,

    /// <summary>
    /// Dedicated Application, Dedicated Storage
    /// </summary>
    DedicatedDedicated
}

