namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityFeature
{
    Task RunAsync();
}

public interface IActivityFeature<TActivityState>
    : IActivityFeature
    where TActivityState : class, IActivityState
{
    TActivityState ActivityState { get; }
}
