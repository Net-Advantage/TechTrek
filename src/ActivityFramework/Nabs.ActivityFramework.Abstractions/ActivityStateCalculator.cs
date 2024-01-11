
namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateCalculator<TActivityState> 
    : IActivityFeature<TActivityState>
    where TActivityState : IActivityState
{

}

public abstract class ActivityStateCalculator<TActivityState>
    : IActivityStateCalculator<TActivityState>
    where TActivityState : IActivityState
{
    public TActivityState ActivityState { get; protected set; } = default!;

    public abstract Task RunAsync(TActivityState activityState);

    
}