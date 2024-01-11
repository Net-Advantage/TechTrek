namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateCalculator<TActivityState>
    : IActivityFeature<TActivityState>
    where TActivityState : class, IActivityState;

public abstract class ActivityStateCalculator<TActivityState>
    : IActivityStateCalculator<TActivityState>
    where TActivityState : class, IActivityState
{
    protected ActivityStateCalculator(TActivityState activityState)
    {
        ActivityState = activityState;
    }

    public TActivityState ActivityState { get; protected set; }

    public abstract Task RunAsync();
}