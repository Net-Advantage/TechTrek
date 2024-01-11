
namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateFactory : IActivityFeature
{
    
}

public abstract class ActivityStateFactory<TActivityState>
    : IActivityStateFactory
    where TActivityState : IActivityState, new()
{
    public TActivityState ActivityState { get; protected set; } = new();

    public abstract Task RunAsync();
}
