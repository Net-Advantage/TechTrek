using System.Diagnostics;

namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateFactory<TActivityState>
    : IActivityFeature<TActivityState>
    where TActivityState : class, IActivityState;

public abstract class ActivityStateFactory<TActivityState>
    : IActivityStateFactory<TActivityState>
    where TActivityState : class, IActivityState
{
    public TActivityState ActivityState { get; protected set; } = default!;

    public abstract Task RunAsync();
}
