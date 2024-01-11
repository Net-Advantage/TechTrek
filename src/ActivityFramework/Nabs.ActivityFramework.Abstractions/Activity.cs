namespace Nabs.ActivityFramework.Abstractions;

public abstract class Activity<TActivityState> 
    : IActivity
    where TActivityState : IActivityState, new()
{
    protected ActivityStateFactory<TActivityState> Factory { get; private set; } = default!;
    public TActivityState? ActivityState => Factory.ActivityState;

    public abstract Task RunAsync();

    protected void AutoAddFeatures()
    {
        //TODO: DWS: Add all the feature by scanning this assembly.
    }

    protected void AddFactory<TStateFactory>()
        where TStateFactory : ActivityStateFactory<TActivityState>, new()
    {
        Factory = new TStateFactory();
    }
}

