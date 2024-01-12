namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateTransformer<TActivityState>
    : IActivityStateBehaviour<TActivityState>
    where TActivityState : class, IActivityState;

public abstract class ActivityStateTransformer<TActivityState>
    : IActivityStateTransformer<TActivityState>
    where TActivityState : class, IActivityState
{
    public abstract Task<TActivityState> RunAsync(TActivityState activityState);
}