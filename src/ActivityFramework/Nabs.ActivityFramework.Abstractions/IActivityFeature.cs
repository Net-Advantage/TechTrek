namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityFeature
{
    Task RunAsync();
}

public interface IActivityFeature<TActivityState>
    where TActivityState : IActivityState
{
    TActivityState ActivityState { get; }

    Task RunAsync(TActivityState activityState);
}
