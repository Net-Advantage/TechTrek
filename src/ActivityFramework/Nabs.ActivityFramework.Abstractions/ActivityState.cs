namespace Nabs.ActivityFramework.Abstractions;

public abstract record ActivityState : IActivityState
{
    public Guid Id { get; set; }
}