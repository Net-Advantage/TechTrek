namespace Nabs.ActivityFramework.Abstractions;

public interface IActivityStateInitialiser<TActivityState>
    where TActivityState : class, IActivityState
{
    Task<TActivityState> RunAsync();
};


/// <summary>
/// Retrieves the state from the specified source or creates a new one.
/// </summary>
/// <typeparam name="TActivityState"></typeparam>
public abstract class ActivityStateInitialiser<TActivityState>
    : IActivityStateInitialiser<TActivityState>
    where TActivityState : class, IActivityState
{
    public abstract Task<TActivityState> RunAsync();
}
