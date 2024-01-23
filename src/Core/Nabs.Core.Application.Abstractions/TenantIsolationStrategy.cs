namespace Nabs.Core.Application.Abstractions;

/// <summary>
/// Described the tenant strategy of the application.
/// </summary>
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