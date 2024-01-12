namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateBehaviour<TActivityState>
    where TActivityState : class, IActivityState
{
    Task<TActivityState> RunAsync(TActivityState activityState);
}
